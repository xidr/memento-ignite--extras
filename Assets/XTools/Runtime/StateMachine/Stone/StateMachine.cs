using System;
using System.Collections.Generic;
using UnityEngine;

namespace XTools.SM.Stone {
    public class StateMachine : MonoBehaviour {
        [SerializeField] Transform _statesRoot;
        
        readonly List<IState> _states = new();
        public IState currentState { get; private set; }

        public void Init(MonoBehaviour core, bool autoSetInitialState) {
            _statesRoot.GetComponentsInChildren(_states);

            _states.ForEach(x => {
                x.OnTransitionRequired += ChangeState;
                x.Init(core);
            });
            if (autoSetInitialState)
                ChangeState(_states[0].GetType()); 
        } 

        public void UpdateSM(float delta) {
            if (_states.Count > 0 && currentState != null)
                currentState.UpdateState(delta);
        }


        public void OnDestroy() {
            _states.ForEach(x => { x.OnTransitionRequired -= ChangeState; });
            
            if (_states.Count > 0 && currentState != null) 
                currentState.Exit();
        }

        public void ChangeState(Type nextStateType) {
            var nextState = _states.Find(x => x.GetType() == nextStateType);

            if (nextState != null) {
                if (currentState != null) 
                    currentState.Exit();
                nextState.Enter();
                currentState = nextState;
            }
        }
    }
}