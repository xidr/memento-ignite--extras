using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XTools {
    internal class MusicModel {
        float _fading;
        AudioSource _current;
        AudioSource _previous;
        readonly MusicData _musicData;
        MusicBundleType _curType;
        List<AudioClip> _curBundle => _musicData.bundles[_curType].audioClips;
        bool _playOnLoop => _musicData.bundles[_curType].shouldLoopFirstClip;
        float _crossFadeTime => _musicData.crossFadeTime;
        readonly MusicSourcesPair _musicSourcesPair;
        bool _sourcesAreReversed = true;

        public MusicModel(MusicData musicData, MusicSourcesPair musicSourcesPair) {
            _musicData = musicData;
            _musicSourcesPair = musicSourcesPair;
        }

        public bool LoadBundle(MusicBundleType bundleType) {
            var check = _curType == bundleType;
            _curType = bundleType;

            return check;
        }

        public void CheckForCrossFade() {
            if (!_current) return;

            if (!_current.clip) return;

            if (_current.clip.length - _current.time <= _crossFadeTime) PlayNextTrack();
        }

        public void PlayNextTrack() {
            CorrectSources();

            if (_playOnLoop) Play(_curBundle[0]);
        }

        public void Play(AudioClip clip) {
            _current.clip = clip;
            _current.loop = false; // For playlist functionality, we want tracks to play once
            _current.volume = 0;
            _current.bypassListenerEffects = true;
            _current.Play();

            GameLoopCenter.Instance.StartCoroutine(HandleCrossFadeExo());
        }

        void CorrectSources() {
            _sourcesAreReversed = !_sourcesAreReversed;
            _previous = _sourcesAreReversed ? _musicSourcesPair.sourceTwo : _musicSourcesPair.sourceOne;
            _current = _sourcesAreReversed ? _musicSourcesPair.sourceOne : _musicSourcesPair.sourceTwo;
        }

        IEnumerator HandleCrossFadeExo() {
            _fading = 0.001f;

            while (_fading > 0f) {
                _fading += Time.deltaTime;

                var fraction = Mathf.Clamp01(_fading / _crossFadeTime);

                // Logarithmic fade
                var logFraction = fraction.ToLogarithmicFraction();

                if (_previous) _previous.volume = 1.0f - logFraction;
                if (_current) _current.volume = logFraction;

                if (fraction >= 1) _fading = 0.0f;

                yield return null;
            }
        }

        public struct MusicSourcesPair {
            public AudioSource sourceOne;
            public AudioSource sourceTwo;
        }
    }
}