// #if UNITY_EDITOR


using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using XTools;

namespace MIE {
    public partial class DifficultyLevel {
        void CalculateDistributionSlidersValues() {
            var changedItem = wordsPossibilitiesDistribution.FirstOrDefault(x => x.wasChanged);

            if (changedItem == null) return;

            if (wordsPossibilitiesDistribution.HasPrevious(changedItem)) {
                var prev = wordsPossibilitiesDistribution.GetPrevious(changedItem);
                if (prev.range.x > changedItem.range.x)
                    changedItem.range.x = prev.range.x;
                else prev.range.y = changedItem.range.x;
            }

            if (wordsPossibilitiesDistribution.HasNext(changedItem)) {
                var next = wordsPossibilitiesDistribution.GetNext(changedItem);
                if (next.range.y < changedItem.range.y)
                    changedItem.range.y = next.range.y;
                else next.range.x = changedItem.range.y;
            }

            changedItem.wasChanged = false;

            wordsPossibilitiesDistribution.First().range.x = 0;
            wordsPossibilitiesDistribution.Last().range.y = 100;
        }

        // public void Validate(SelfValidationResult result) {
        //     if (wordsPossibilitiesDistribution.Length == 0)
        //         result.AddError($"'{nameof(wordsPossibilitiesDistribution)}' is empty list!").WithFix(() =>
        //             wordsPossibilitiesDistribution.Add(new DifficultyDistribution {
        //                 minMaxValueSlider = new Vector2(0, 100)
        //             }));
        // }
    }

    public partial class GameDataSO : ISelfValidator {
    
    
        
        public void Validate(SelfValidationResult result) {
            for (int i = 0; i < difficultyLevels.Length - 1; i++) {
                if (difficultyLevels[i].startTime > difficultyLevels[i + 1].startTime)
                    result.AddError("Start time of level " + i + " is greater than start time of level " + (i + 1));
            }

        }
    }
}
// #endif