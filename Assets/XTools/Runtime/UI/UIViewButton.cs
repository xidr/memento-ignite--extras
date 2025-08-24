using Reflex.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XTools {
    [RequireComponent(typeof(Button))]
    public class UIViewButton : MonoBehaviour {
        [SerializeField] XToolsEvents.UIButtonTypes _buttonType;
        [SerializeField] bool _hasSounds;

        [SerializeField] [ShowIf("_hasSounds")]
        SoundData _onHoverSoundData;

        [SerializeField] [ShowIf("_hasSounds")]
        SoundData _onClickSoundData;

        [Inject] AudioManager _audioManager;
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
            XToolsEvents.UIButtonPressed.Invoke(_buttonType);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (_hasSounds) _audioManager.PlaySound(_onHoverSoundData);
        }
    }
}