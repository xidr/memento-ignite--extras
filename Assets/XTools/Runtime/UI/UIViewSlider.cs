using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace XTools {
    [RequireComponent(typeof(Slider))]
    public class UIViewSlider : MonoBehaviour, IVisitable {
        public UIAudioSliderChanged.UIAudioSliders sliderType;
        Slider _sliderReference;
        

        void Awake() {
            _sliderReference = GetComponent<Slider>();
        }
        
        void OnEnable() {
            _sliderReference.onValueChanged.AddListener(delegate {
                RaiseUIAudioSliderEvent();
            });
        }
        
        void OnDisable() {
            _sliderReference.onValueChanged.RemoveListener(delegate {
                RaiseUIAudioSliderEvent();
            });
        }

        void RaiseUIAudioSliderEvent() {
            EventBus<UIAudioSliderChanged>.Raise(new UIAudioSliderChanged
            {
                audioSliderType = sliderType,
                value = _sliderReference.value,
            });
        }

        
        public void Accept(IVisitor visitor) {
            visitor.Visit(this);
        }

        public void SetValue(float value) {
            switch (sliderType) {
                case UIAudioSliderChanged.UIAudioSliders.MusicVolume: {
                    _sliderReference.value = value;
                    break;
                }
                case UIAudioSliderChanged.UIAudioSliders.SfxVolume: {
                    _sliderReference.value = value;
                    break;
                }
            }
        }
    }
}