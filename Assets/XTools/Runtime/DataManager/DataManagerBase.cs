using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XTools {
    public abstract class DataManagerBase : IDisposable {
        List<ScriptableObject> _data = new();

        EventBinding<UIAudioSliderChanged> _uiAudioSliderChangedBinding;

        public DataManagerBase() {
            ScriptableObject[] loaded = Resources.LoadAll<ScriptableObject>("GameData");

            _data.AddRange(loaded);

            // ServiceLocator.Global.Register(typeof(IVisitorDataSupplier), this);

            _uiAudioSliderChangedBinding = new EventBinding<UIAudioSliderChanged>(UiAudioSliderChanged);
            EventBus<UIAudioSliderChanged>.Register(_uiAudioSliderChangedBinding);
        }

        public void Initialize() {
            GameLoopCenter.Instance.SubscribeForDispose(this);
        }

        public void Dispose() {
            EventBus<UIAudioSliderChanged>.Deregister(_uiAudioSliderChangedBinding);
        }

        public virtual T GetData<T>() where T : ScriptableObject {
            ScriptableObject foundData = _data.FirstOrDefault(x => x.GetType() == typeof(T));

            T foundDataT = foundData as T;
            if (foundDataT == null) {
                Debug.LogError(typeof(T).Name + " could not be loaded");
            }

            return foundDataT;
        }

        void UiAudioSliderChanged(UIAudioSliderChanged evt) {
            var audioData = GetData<AudioDataSO>();
            switch (evt.audioSliderType) {
                case UIAudioSliderChanged.UIAudioSliders.MusicVolume: {
                    audioData.musicVolume = evt.value;
                    break;
                }
                case UIAudioSliderChanged.UIAudioSliders.SfxVolume: {
                    audioData.sfxVolume = evt.value;
                    break;
                }
            }

            EventBus<DataChanged>.Raise(new DataChanged());
        }
    }
}