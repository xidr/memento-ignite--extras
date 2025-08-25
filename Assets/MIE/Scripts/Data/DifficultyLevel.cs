using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MIE {
    [Serializable]
    public partial class DifficultyLevel {

        [Range(0, 3600)]
        public int startTime;

        [Range(-120, 20)]
        public int temperature;
        
        [HideReferenceObjectPicker]
        [ListDrawerSettings(ListElementLabelName = "correspondingContainerName", IsReadOnly = true)]
        [OnValueChanged("CalculateDistributionSlidersValues", true)]
        public DifficultyDistribution[] wordsPossibilitiesDistribution =
            new DifficultyDistribution[MIEConstants.WORDS_BUNDLES_COUNT];

        public DifficultyLevel() {
            for (var i = 0; i < wordsPossibilitiesDistribution.Length; i++)
                wordsPossibilitiesDistribution[i] = new DifficultyDistribution(MIEConstants.WORDS_BUNDLES_NAMES[i],
                    new Vector2(
                        100f * i / wordsPossibilitiesDistribution.Length,
                        100f * (i + 1) / wordsPossibilitiesDistribution.Length));
        }


        
    }

    // [Serializable]
    // public class WordsContainer {
    //     public readonly string[] words;
    //     
    //     public WordsContainer(string[] words
    //     ) {
    //         this.words = words;
    //     }
    // }

    [Serializable]
    public class DifficultyDistribution {

        // [BoxGroup("@correspondingContainerName")]
        [OnValueChanged("SetChanged")] [MinMaxSlider(0, 100, true)]
        public Vector2 range = new(0, 10);
        public string correspondingContainerName { get; private set; }

        public DifficultyDistribution(string correspondingContainerName, Vector2 range) {
            this.correspondingContainerName = correspondingContainerName;
            this.range = range; 
        }

        [HideInInspector] public bool wasChanged;
        void SetChanged() => wasChanged = true;

    }
}