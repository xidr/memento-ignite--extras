using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = System.Object;

namespace XTools.SM.Silver {
    public interface IState {

        IState parent { get; set; }
        IState activeChild { get; set; }
        StateMachine machine { get; set; }
        IReadOnlyList<IActivity> activities { get; }


        void Enter();
        void Exit();
        void Update(float deltaTime);
        List<IState> GetChildren();
        void SetupRecursively(StateMachine machine, IState parent = null, Object context = null);
        IEnumerable<IState> PathToRoot();
        IState Leaf();

    }

    public interface IRootState : IState { }

    [Serializable]
    public abstract class State<TContext, TTarget> : IState, ISelfValidator where TTarget : IState {

        public IState activeChild { get; set; }

        [FoldoutGroup("Base")]
        // [HorizontalGroup("Base/Split", width: 0.25f)]
        [TabGroup("Base/Tabs", "Other", order: 3)]
        [VerticalGroup("Base/Tabs/Other/Vertical")]
        [BoxGroup("Base/Tabs/Other/Vertical/Info")]
        [ShowInInspector] [ReadOnly]
        public IState parent { get; set; }

        // public IState parent => _parent;
        [BoxGroup("Base/Tabs/Other/Vertical/Info")]
        [ShowInInspector] [ReadOnly]
        public StateMachine machine { get; set; }

        [BoxGroup("Base/Tabs/Other/Vertical/Settings")]
        [SerializeField] [ValueDropdown("GetObjectOptions")]
        // [ValidateInput("ValidateInitialIndex")]
        int _initialStateIndex = -1;

        readonly List<IActivity> _activities = new();

        public IReadOnlyList<IActivity> activities => _activities;

        // [VerticalGroup("Base/Split/Selection")]
        [TabGroup("Base/Tabs", "Transitions", order: 2)]
        [SerializeField] [OnValueChanged("SetTransitionsParent")]
        List<Transition> _transitions = new();

        // [VerticalGroup("Base/Split/Selection")]
        [TabGroup("Base/Tabs", "Children", order: 1)]
        [SerializeField]
        List<TTarget> _children = new();

        [SerializeField]
        [ShowIf("_isRoot")]
        protected TContext _context;
        bool _isRoot => this is IRootState;


        public void AddActivity(IActivity a) {
            if (a != null) _activities.Add(a);
        }

        protected virtual IState GetInitialState() => _initialStateIndex >= 0
            ? _children[_initialStateIndex]
            : null; // Initial child to enter when this state starts (null = this is the leaf)

        protected Transition GetTransition() {
            // foreach (var transition in anyTransitions)
            //     if (transition.Evaluate())
            //         return transition;

            foreach (var transition in _transitions)
                if (transition.Evaluate())
                    return transition;

            return null;
        } // Target state to switch to this frame (null = stay in the current state)

        // Lifecycle hooks
        protected virtual void OnEnter() { }
        protected virtual void OnExit() { }
        protected virtual void OnUpdate(float deltaTime) { }

        public void SetupRecursively(StateMachine machine, IState parent = null, Object context = null) {
            this.machine = machine;
            this.parent = parent;

            if (context != null && context is TContext ctx) _context = ctx;

            foreach (var child in _children) child.SetupRecursively(machine, this, _context);
        }

        public void Enter() {
            if (parent != null) parent.activeChild = this;
            foreach (var transition in _transitions) transition.Enable();
            OnEnter();
            var init = GetInitialState();
            if (init != null) init.Enter();
        }

        public void Exit() {
            if (activeChild != null) activeChild.Exit();
            activeChild = null;
            foreach (var transition in _transitions) transition.Disable();
            OnExit();
        }

        public void Update(float deltaTime) {
            var t = GetTransition();
            if (t != null) {
                machine.sequencer.RequestTransition(_children[t.from], _children[t.to]);
                return;
            }

            if (activeChild != null) activeChild.Update(deltaTime);
            OnUpdate(deltaTime);
        }

        public List<IState> GetChildren() {
            List<IState> trueChildren = new();
            foreach (var child in _children)
                if (child != null)
                    trueChildren.Add(child);
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
            for (var s = this as IState; s != null; s = s.parent) yield return s;
        }


        IEnumerable<ValueDropdownItem<int>> GetObjectOptions() {
            return _children
                .Select((obj, index) => new ValueDropdownItem<int>(
                    obj != null ? obj.ToString() : "NULL",
                    index))
                .Prepend(new ValueDropdownItem<int>("None", -1));
        }

        void SetTransitionsParent() {
            Debug.Log(GetChildren());
            foreach (var transition in _transitions) transition.SetParent(this);
        }

        // bool ValidateInitialIndex() {
        //     return _initialStateIndex >= children.Count - 1;
        // }

        public void Validate(SelfValidationResult result) {
            if (_initialStateIndex >= _children.Count)
                result.AddError("Initial state index is out of range").WithFix(() => _initialStateIndex = -1);
            if (_transitions == null)
                result.AddError("Transitions is null").WithFix(() => _transitions = new List<Transition>());
            if (_isRoot && _context == null) result.AddError($"Context in {GetType()} state is null");

            
            foreach (var transition in _transitions) {
                var validationData = transition.GetValidationData();
                if (validationData.Count > 0) validationData.ForEach(x => result.AddError(x));
            }
        }

    }
}