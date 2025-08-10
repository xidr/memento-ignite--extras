using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace XTools {
    [CreateAssetMenu(fileName = "AudioDataSO", menuName = "XTools/AudioDataSO", order = 0)]
    public class AudioDataSO : SerializedScriptableObject {
        [Range(0.01f, 1f)]
        public float musicVolume;
        [Range(0.01f, 1f)]
        public float sfxVolume;
        [NonSerialized, OdinSerialize]
        public MusicData music;
    }
    
    [Serializable]
    public class MusicData {
        // public List<AudioClip> audioClips;
        [OdinSerialize]
        public Dictionary<MusicBundleType, MusicBundle> bundles = new();

        public float crossFadeTime = 2.0f;
    }

    [Serializable]
    public class MusicBundle {
        public List<AudioClip> audioClips;
        public bool shouldLoopFirstClip;
    }

    public enum MusicBundleType {
        MainMenu,
        Gameplay
    }
}