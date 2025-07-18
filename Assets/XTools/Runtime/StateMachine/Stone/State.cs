using System;
using UnityEngine;

namespace XTools.SM.Stone {
    public interface IState {
        public event Action<Type> OnTransitionRequired;

        public void Init(MonoBehaviour core);

        public void UpdateState(float delta);

        public void Enter();

        public void Exit();

        public void RequestTransition<T>() where T : IState;
    }

    public abstract class State<TContextType> : MonoBehaviour, IState where TContextType : MonoBehaviour {
        public event Action<Type> OnTransitionRequired;

        protected TContextType _core;

        public virtual void Init(MonoBehaviour core) {
            _core = (TContextType)core;
            gameObject.SetActive(false);
        }

        public virtual void UpdateState(float delta) { }

        public virtual void Enter() {
            gameObject.SetActive(true);
            OnEnter();
        }

        public virtual void Exit() {
            OnExit();
            gameObject.SetActive(false);
        }

        protected virtual void OnEnter() { }

        protected virtual void OnExit() { }

        public void RequestTransition<T>() where T : IState {
            OnTransitionRequired?.Invoke(typeof(T));
        }
    }
}