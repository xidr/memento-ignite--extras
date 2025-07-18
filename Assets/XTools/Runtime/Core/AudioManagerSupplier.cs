using System;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Audio;

namespace XTools {
    public class AudioManagerSupplier : MonoBehaviour {
        [Inject] DataManagerBase _dataManagerBase; 
        [Inject] AudioManager _audioManager;
        
        [SerializeField] AudioMixer _mixer;
        [SerializeField] AudioSource[] _musicSources = new AudioSource[2];
        [SerializeField] SoundModel _soundModel;

        void Awake() {
            _audioManager.Initialize(new AudioManager.InitData()
            {
                dataManager = _dataManagerBase,
                mixer = _mixer,
                musicSources = _musicSources,
                soundModel = _soundModel
            });
        }
    }
}