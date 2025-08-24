using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace XTools.SM.Silver {
    public class StateMachine : SerializedMonoBehaviour {
        public TransitionSequencer sequencer { get; private set; }

        [NonSerialized, OdinSerialize] IRootState _root;

        bool _started;


        // void Start() {
        //     StartSM();
        // }

        void StartSM() {
            if (_started) return;

            sequencer = new TransitionSequencer(this);
            Wire(_root, this, new HashSet<IState>());
            _started = true;
            _root.Enter();
        }

        // Separate those for introducing sequencing
        public void Tick(float deltaTime) {
            if (!_started) StartSM();

            sequencer.Tick(deltaTime);
        }

        internal void InternalTick(float deltaTime) => _root.Update(deltaTime);

        // Perform the actual switch from 'from' to 'to' by exiting up to the shared ancestor, then entering down to the target
        public void ChangeState(IState from, IState to) {
            if (from == to || from == null || to == null) return;

            var lca = TransitionSequencer.Lca(from, to);

            // Exit current branch up to (but not including) LCA
            for (var s = from; s != lca; s = s.parent) s.Exit();

            // Enter target branch from LCA down to target
            var stack = new Stack<IState>();
            for (var s = to; s != lca; s = s.parent) stack.Push(s);
            while (stack.Count > 0) stack.Pop().Enter();
        }

        void Wire(IState s, StateMachine m, HashSet<IState> visited, IState parent = null) {
            if (s == null) return;
            if (!visited.Add(s)) return; // State is already visited

            var flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.FlattenHierarchy;
            var machineField = typeof(IState).GetField("_machine", flags);
            if (machineField != null) machineField.SetValue(s, m);

            foreach (var fld in s.GetType().GetFields(flags)) {
                if (typeof(IState).IsAssignableFrom(fld.FieldType))
                    // Only consider fields that are State
                    if (fld.Name == "_parent") {
                        if (parent != null) fld.SetValue(s, parent);
                        continue; // Skip back-edge to parent
                    }


                // Check if field is a collection of States
                if (IsStateList(fld.FieldType)) {
                    var stateType = fld.FieldType.GetGenericArguments()[0];
                    // Debug.Log($"Found List<{stateType.Name}>");

                    var list = fld.GetValue(s);
                    // Cast to IEnumerable to iterate
                    if (list is IEnumerable enumerable)
                        foreach (var item in enumerable)
                            if (item is IState state)
                                Wire(state, m, visited, s); // Recurse into the child
                }
            }
        }

        static bool IsStateList(Type type) {
            if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(List<>)) return false;

            var elementType = type.GetGenericArguments()[0];
            return typeof(IState).IsAssignableFrom(elementType);
        }
    }
}