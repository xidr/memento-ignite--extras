using UnityEngine;

namespace XTools {
    public class SoundBuilder {
        readonly SoundModel _soundModel;
        Vector3 _position = Vector3.zero;
        bool _randomPitch;

        public SoundBuilder(SoundModel soundModel) {
            _soundModel = soundModel;
        }

        public SoundBuilder WithPosition(Vector3 position) {
            _position = position;
            return this;
        }

        public SoundBuilder WithRandomPitch() {
            _randomPitch = true;
            return this;
        }

        public SoundEmitter Play(SoundData soundData) {
            if (soundData == null) {
                Debug.LogError("SoundData is null");
                return null;
            }

            if (!_soundModel.CanPlaySound(soundData)) return null;

            SoundEmitter soundEmitter = _soundModel.Get();
            soundEmitter.Initialize(soundData, _soundModel);
            soundEmitter.transform.position = _position;
            soundEmitter.transform.parent = _soundModel.transform;

            if (_randomPitch) soundEmitter.WithRandomPitch();

            if (soundData.frequentSound) soundEmitter.node = _soundModel.frequentSoundEmitters.AddLast(soundEmitter);

            soundEmitter.Play();

            return soundEmitter;
        }
    }
}