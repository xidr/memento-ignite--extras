using System;
using Alchemy.Serialization;
using UnityEngine;

namespace XTools {
    [AlchemySerialize]
    // Don't forget to add it:
    // [CreateAssetMenu(fileName = "GameDataSO", menuName = "XTools/GameDataSO", order = 0)]
    public partial class GameDataSOBase : ScriptableObject {
        [AlchemySerializeField, NonSerialized]
        public AudioDataSO audio;
    }
}