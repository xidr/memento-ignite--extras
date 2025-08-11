using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using XTools;

namespace MIE {
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "MIE/GameDataSO", order = 0)]
    public partial class GameDataSO : SerializedScriptableObject {
        // [SerializeField]
        // [MinMaxSlider(-10, 10, true)]
        // Vector2 WithFields = new Vector2(-3, 4);
        //
        // [ValidateInput("MustBeNull", "This field should be null.")]
        // [ValidateInput("@_range1.y < _range2.x", "Check", InfoMessageType.Warning)]
        // [SerializeField]
        // // [MinMaxSlider(0, "@_range2.x", true)]
        // Vector2 _range1 = new Vector2(0, 450);
        //
        // [SerializeField]
        // // [MinMaxSlider("@_range1.y", "@_range3.x", true)]
        // [MinMaxSlider(0, 450, true)]
        // Vector2 _range2 = new Vector2(0, 450);
        //
        // [SerializeField]
        // [MinMaxSlider("@_range2.y", 450, true)]
        // Vector2 _range3 = new Vector2(0, 450);

        [SerializeField] [OnValueChanged("Calculate", includeChildren: true)]
        List<DifficultyDistribution> _difficulties = new List<DifficultyDistribution>();

        public int listCount;
    }

    [Serializable]
    public class DifficultyDistribution {
        [OnValueChanged("SetChanged")] [MinMaxSlider(0, 100, true)]
        public Vector2 minMaxValueSlider = new Vector2(0, 10);

        [HideInInspector] public bool wasChanged;
        void SetChanged() => wasChanged = true;
    }
}