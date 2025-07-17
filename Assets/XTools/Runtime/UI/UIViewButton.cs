using System;
using UnityEngine;
using UnityEngine.UI;

namespace XTools {
    [RequireComponent(typeof(Button))]
    public class UIViewButton : MonoBehaviour {
        [SerializeField] UIButtonPressed.UIButtons _buttonType;
        Button _buttonReference;

        void Awake() {
            _buttonReference = GetComponent<Button>();
        }

        void OnEnable() {
            _buttonReference.onClick.AddListener(RaiseUIButtonEvent);
        }
        
        void OnDisable() {
            _buttonReference.onClick.RemoveListener(RaiseUIButtonEvent);
        }

        void RaiseUIButtonEvent() {
            EventBus<UIButtonPressed>.Raise(new UIButtonPressed {
                buttonType = _buttonType,
            });
        }
    }
}