using System;
using System.Collections.Generic;
using UnityEngine;

namespace XTools {
    public abstract class SceneState<TContextType> : State<TContextType> where TContextType : MonoBehaviour {
        [SerializeField] protected List<GameObject> _views;

        EventBinding<UIButtonPressed> _UIButtonPressedBinding;
        protected SceneLoader _sceneLoader;
        
        public override void Init(MonoBehaviour context) {
            base.Init(context);
            SetViewsVisibility(false);
            _UIButtonPressedBinding = new EventBinding<UIButtonPressed>(OnUIButtonPressed);
            ServiceLocator.For(this).Get(out _sceneLoader);
        }

        protected override void OnEnter() {
            SetViewsVisibility(true);
            EventBus<UIButtonPressed>.Register(_UIButtonPressedBinding);
        }

        protected override void OnExit() {
            SetViewsVisibility(false);
            EventBus<UIButtonPressed>.Deregister(_UIButtonPressedBinding);
        }

        void SetViewsVisibility(bool visibility) {
            _views?.ForEach(x => {
                // Debug.Log(x.gameObject.name);
                if (x)
                    x.SetActive(visibility);
            });
        }
        
        protected virtual void OnUIButtonPressed(UIButtonPressed evt) {}
    }
}