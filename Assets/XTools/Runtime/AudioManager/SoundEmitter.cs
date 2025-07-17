using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XTools {
    [RequireComponent(typeof(AudioSource))]
    public class SoundEmitter : MonoBehaviour {
        public SoundData soundData { get; private set; }
        public LinkedListNode<SoundEmitter> node { get; set; }

        AudioSource _audioSource;
        Coroutine _playingCoroutine;
        SoundModel _model;

        void Awake() {
            _audioSource = gameObject.GetOrAdd<AudioSource>();
        }

        public void Initialize(SoundData data, SoundModel model) {
            _model = model;

            soundData = data;
            // audioSource.clip = data.clip;
            _audioSource.outputAudioMixerGroup = data.mixerGroup;
            _audioSource.playOnAwake = data.playOnAwake;

            _audioSource.mute = data.mute;
            _audioSource.bypassEffects = data.bypassEffects;
            _audioSource.bypassListenerEffects = data.bypassListenerEffects;
            _audioSource.bypassReverbZones = data.bypassReverbZones;

            _audioSource.priority = data.priority;
            _audioSource.volume = data.volume;
            _audioSource.pitch = data.pitch;
            _audioSource.panStereo = data.panStereo;
            _audioSource.spatialBlend = data.spatialBlend;
            _audioSource.reverbZoneMix = data.reverbZoneMix;
            _audioSource.dopplerLevel = data.dopplerLevel;
            _audioSource.spread = data.spread;

            _audioSource.minDistance = data.minDistance;
            _audioSource.maxDistance = data.maxDistance;

            _audioSource.ignoreListenerVolume = data.ignoreListenerVolume;
            _audioSource.ignoreListenerPause = data.ignoreListenerPause;

            _audioSource.rolloffMode = data.rolloffMode;

            _audioSource.loop = data.shouldPlayOnLoop;
        }

        public void Play() {
            if (_playingCoroutine != null) StopCoroutine(_playingCoroutine);

            if (soundData.clips.Count > 0) _audioSource.clip = soundData.clips[Random.Range(0, soundData.clips.Count)];

            _audioSource.Play();
            _playingCoroutine = StartCoroutine(WaitForSoundToEnd());
        }

        IEnumerator WaitForSoundToEnd() {
            yield return new WaitWhile(() => _audioSource.isPlaying);
            Stop();
        }

        public void Stop() {
            if (_playingCoroutine != null) {
                StopCoroutine(_playingCoroutine);
                _playingCoroutine = null;
            }

            _audioSource.Stop();
            _model.ReturnToPool(this);
        }

        public void WithRandomPitch(float min = -0.1f, float max = 0.1f) {
            _audioSource.pitch += Random.Range(min, max);
        }
    }
}