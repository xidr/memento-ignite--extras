using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace XTools.SM.Silver {
    public interface IState {
        IState parent { get; } 
        IState activeChild { get; set; } 
        public IReadOnlyList<IActivity> activities { get; }

        void Enter();
        void Exit();
        void Update(float deltaTime);
        List<IState> GetChildren();
    }
    
    [Serializable]
    public abstract class State<TContext, TTarget> : IState where TTarget : IState{
        
        public IState activeChild { get; set; }
        

        
        [ShowInInspector, ReadOnly] readonly State<TContext, TTarget> _parent;
        public IState parent => _parent;
        [ShowInInspector, ReadOnly] readonly StateMachine _machine;
        readonly List<IActivity> _activities = new();
        public IReadOnlyList<IActivity> activities => _activities;
        
        [OdinSerialize, OnValueChanged("SetTransitionsParent")]
        HashSet<Transition> transitions = new();
        [OdinSerialize] List<TTarget> children = new();
        [OdinSerialize, ValueDropdown("GetObjectOptions")]
        TTarget _initialState;
        

        // public State(StateMachine machine, State parent = null) {
        //     this.machine = machine;
        //     this.parent = parent;
        // }

        public void AddActivity(IActivity a) {
            if (a != null) _activities.Add(a);
        }

        protected virtual State<TContext, TTarget> GetInitialState() =>
            null; // Initial child to enter when this state starts (null = this is the leaf)

        protected Transition GetTransition() {
            // foreach (var transition in anyTransitions)
            //     if (transition.Evaluate())
            //         return transition;

            foreach (var transition in transitions) {
                if (transition.Evaluate()) return transition;
            }

            return null;
        } // Target state to switch to this frame (null = stay in the current state)

        // Lifecycle hooks
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected virtual void OnUpdate(float deltaTime) { }

        public void Enter() {
            if (_parent != null) _parent.activeChild = this;
            OnEnter();
            var init = GetInitialState();
            if (init != null) init.Enter();
        }

        public void Exit() {
            if (activeChild != null) activeChild.Exit();
            activeChild = null;
            OnExit();
        }

        public void Update(float deltaTime) {
            var t = GetTransition();
            if (t != null) {
                _machine.sequencer.RequestTransition(this, t.to);
                return;
            }

            if (activeChild != null) activeChild.Update(deltaTime);
            OnUpdate(deltaTime);
        }

        public List<IState> GetChildren() {
            List<IState> trueChildren = new();
            foreach (var child in children) {
                if (child != null) trueChildren.Add(child);
            }
            return trueChildren;
        }

        // Returns the deepest currently-active descendant state (the leaf of the active path)
        public IState Leaf() {
            IState s = this;
            while (s.activeChild != null) s = s.activeChild;
            return s;
        }

        // Yields this state and then each ancestor up to the root (self -> parent -> ... -> root)
        public IEnumerable<IState> PathToRoot() {
            for (var s = this; s != null; s = s._parent) yield return s;
        }
        
        
        IEnumerable<ValueDropdownItem<TTarget>> GetObjectOptions()
        {
            return children
                .Where(obj => obj != null)
                .Select(obj => new ValueDropdownItem<TTarget>(obj.ToString(), obj));
        }

        void SetTransitionsParent() {
            Debug.Log(GetChildren());
            foreach (var transition in transitions) {
                transition.SetParent(this);
            }
        }
    }
}