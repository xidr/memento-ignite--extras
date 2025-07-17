using System;
using System.Collections.Generic;
using Alchemy.Serialization;
using UnityEngine;

namespace XTools {
    [AlchemySerialize]
    [CreateAssetMenu(fileName = "AudioDataSO", menuName = "XTools/AudioDataSO", order = 0)]
    public partial class AudioDataSO : ScriptableObject {
        [Range(0.01f, 1f)]
        public float musicVolume;
        [Range(0.01f, 1f)]
        public float sfxVolume;
        [AlchemySerializeField, NonSerialized]
        public MusicData music;
    }
    
    [AlchemySerialize]
    public partial class MusicData {
        // public List<AudioClip> audioClips;
        [AlchemySerializeField, NonSerialized]
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