using System;
using UnityEngine;

namespace MIE {
    [Serializable]
    [CreateAssetMenu(fileName = "GameDataSO2", menuName = "XTools/GameDataSO2", order = 0)]
    public sealed class TestData : GameDataBase {
        
        public int testInt;
        
    }
}

