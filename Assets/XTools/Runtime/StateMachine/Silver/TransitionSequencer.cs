using System;
using System.Collections.Generic;
using System.Threading;

namespace XTools.SM.Silver {
    public class TransitionSequencer {
        public readonly StateMachine machine;

        ISequence _sequencer; // current phase (deactivate or activate)
        Action _nextPhase; // switch structure between phases
        (IState from, IState to)? _pending; // coalesce a single pending request
        IState _lastFrom, _lastTo;

        public TransitionSequencer(StateMachine machine) {
            this.machine = machine;
        }

        // Request a transition from one IState to another
        public void RequestTransition(IState from, IState to) {
            // machine.ChangeState(from, to);
            if (to == null || from == null) return;
            if (_sequencer != null) {
                _pending = (from, to);
                return;
            }

            BeginTransition(from, to);
        }

        static List<PhaseStep> GatherPhaseSteps(List<IState> chain, bool deactivate) {
            var steps = new List<PhaseStep>();

            for (var i = 0; i < chain.Count; i++) {
                var acts = chain[i].activities;

                for (var j = 0; j < acts.Count; j++) {
                    var a = acts[j];

                    if (deactivate)
                        if (a.mode == ActivityNode.Active) {
                            steps.Add(ct => a.DeactivateAsync(ct));
                        } else {
                            if (a.mode == ActivityNode.Inactive) steps.Add(ct => a.ActivateAsync(ct));
                        }
                }
            }

            return steps;
        }

        // States to exit: from -> ... up to (but excluding) lca; bottom -> up order
        static List<IState> StatesToExit(IState from, IState lca) {
            var list = new List<IState>();
            for (var s = from; s != null && s != lca; s = s.parent) list.Add(s);
            return list;
        }

        // States to enter: path from 'to' up to (but excluding) lca; returned in enter order (top -> down)
        static List<IState> StatesToEnter(IState to, IState lca) {
            // Use stack to then have correct top-down order
            var stack = new Stack<IState>();
            for (var s = to; s != null && s != lca; s = s.parent) stack.Push(s);
            return new List<IState>(stack);
        }

        CancellationTokenSource _cts;
        public readonly bool useSequential = true;

        void BeginTransition(IState from, IState to) {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var lca = Lca(from, to);
            var exitChain = StatesToExit(from, lca);
            var enterChain = StatesToEnter(to, lca);

            // 1. Deactivate the "old branch"
            var exitSteps = GatherPhaseSteps(exitChain, true);
            // _sequencer = new NoopPhase();
            _sequencer = useSequential
                ? new SequentialPhase(exitSteps, _cts.Token)
                : new ParallelPhase(exitSteps, _cts.Token);
            _sequencer.Start();

            _nextPhase = () => {
                // 2. Change IState
                machine.ChangeState(from, to);
                // 3. Activate the "new branch"
                var enterSteps = GatherPhaseSteps(enterChain, false);
                // _sequencer = new NoopPhase();
                _sequencer = useSequential
                    ? new SequentialPhase(enterSteps, _cts.Token)
                    : new ParallelPhase(enterSteps, _cts.Token);
                _sequencer.Start();
            };
        }

        void EndTransition() {
            _sequencer = null;

            if (_pending.HasValue) {
                var p = _pending.Value;
                _pending = null;
                BeginTransition(p.from, p.to);
            }
        }

        public void Tick(float deltaTime) {
            if (_sequencer != null) {
                if (_sequencer.Update()) {
                    if (_nextPhase != null) {
                        var n = _nextPhase;
                        _nextPhase = null;
                        n();
                    } else {
                        EndTransition();
                    }
                }

                return; // while transitioning, we don't run normal updates
            }

            machine.InternalTick(deltaTime); // so we call it if no transition is in progress
        }

        // Compute the Lowest Common Ancestor of two states
        public static IState Lca(IState a, IState b) {
            // Create a set of all the parents of 'a'
            var ap = new HashSet<IState>();
            for (var s = a; s != null; s = s.parent) ap.Add(s);
            // Find the first parent of 'b' that is also a parent of 'a'
            for (var s = b; s != null; s = s.parent)
                if (ap.Contains(s))
                    return s;
            // If no common ancestor was found, return null
            return null;
        }
    }
}