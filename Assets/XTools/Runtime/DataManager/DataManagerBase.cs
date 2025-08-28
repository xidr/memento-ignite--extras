using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace XTools {
    public abstract class DataManagerBase : IDisposable {
        const string GAME_DATA_GROUP_LABEL = "GameData";
        
        List<ScriptableObject> _data = new();

        EventBinding<UIAudioSliderChanged> _uiAudioSliderChangedBinding;

        public DataManagerBase() {
            // ScriptableObject[] loaded = Resources.LoadAll<ScriptableObject>("GameData");
            //
            // _data.AddRange(loaded);
            
            // Load the entire GameData group synchronously


            // // Don't forget to release
            // Addressables.Release(handle);

            
            // ServiceLocator.Global.Register(typeof(IVisitorDataSupplier), this);

            LoadDataClasses();

            _uiAudioSliderChangedBinding = new EventBinding<UIAudioSliderChanged>(UiAudioSliderChanged);
            EventBus<UIAudioSliderChanged>.Register(_uiAudioSliderChangedBinding);
        }

        public void Initialize() {
            GameLoopCenter.Instance.SubscribeForDispose(this);
        }

        public void Dispose() {
            EventBus<UIAudioSliderChanged>.Deregister(_uiAudioSliderChangedBinding);
        }

        void LoadDataClasses() {
            var handle = Addressables.LoadAssetsAsync<ScriptableObject>(GAME_DATA_GROUP_LABEL, null);
            var loaded = handle.WaitForCompletion();
            
            _data.AddRange(loaded);
            
            
            
            handle.Release();
            
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