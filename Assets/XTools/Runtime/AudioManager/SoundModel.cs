using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace XTools {
    public class SoundModel : MonoBehaviour {
        public readonly LinkedList<SoundEmitter> frequentSoundEmitters = new();
        
        public AudioMixer mixer;
        [HideInInspector]
        public bool initialized = false;
        
        [SerializeField] SoundEmitter _soundEmitterPrefab;
        [SerializeField] bool _collectionCheck = true;
        [SerializeField] int _defaultCapacity = 10;
        [SerializeField] int _maxPoolSize = 100;
        [SerializeField] int _maxSoundInstances = 30;


        IObjectPool<SoundEmitter> _soundEmitterPool;
        readonly List<SoundEmitter> _activeSoundEmitters = new();

        void Start() {
            InitializePool();
            initialized = true;
        }

        public SoundBuilder CreateSoundBuilder() => new SoundBuilder(this);

        public bool CanPlaySound(SoundData data) {
            if (!data.frequentSound) return true;

            if (frequentSoundEmitters.Count >= _maxSoundInstances) {
                try {
                    frequentSoundEmitters.First.Value.Stop();
                    return true;
                } catch {
                    Debug.Log("SoundEmitter is already released");
                }
                return false;
            }
            return true;
        }

        public SoundEmitter Get() {
            if (!initialized)
                return null;
            return _soundEmitterPool.Get();
        }

        public void ReturnToPool(SoundEmitter soundEmitter) {
            _soundEmitterPool.Release(soundEmitter);
        }

        public void StopAll() {
            foreach (var soundEmitter in _activeSoundEmitters) {
                soundEmitter.Stop();
            }

            frequentSoundEmitters.Clear();
        }

        void InitializePool() {
            _soundEmitterPool = new ObjectPool<SoundEmitter>(
                CreateSoundEmitter,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                _collectionCheck,
                _defaultCapacity,
                _maxPoolSize);
        }

        SoundEmitter CreateSoundEmitter() {
            var soundEmitter = Instantiate(_soundEmitterPrefab);
            soundEmitter.gameObject.SetActive(false);
            return soundEmitter;
        }

        void OnTakeFromPool(SoundEmitter soundEmitter) {
            soundEmitter.gameObject.SetActive(true);
            _activeSoundEmitters.Add(soundEmitter);
        }

        void OnReturnedToPool(SoundEmitter soundEmitter) {
            if (soundEmitter.node != null) {
                frequentSoundEmitters.Remove(soundEmitter.node);
                soundEmitter.node = null;
            }
            soundEmitter.gameObject.SetActive(false);
            _activeSoundEmitters.Remove(soundEmitter);
        }

        void OnDestroyPoolObject(SoundEmitter soundEmitter) {
            Destroy(soundEmitter.gameObject);
        }
    }
}