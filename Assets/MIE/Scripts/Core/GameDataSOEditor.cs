using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using XTools;

namespace MIE {
    public partial class GameDataSO : ISelfValidator {
        
        void Calculate() {
            var changedItem = _difficulties.Find(x => x.wasChanged);

            if (changedItem == null) return;

            if (_difficulties.HasPrevious(changedItem)) {
                var prev = _difficulties.GetPrevious(changedItem);
                if (prev.minMaxValueSlider.x > changedItem.minMaxValueSlider.x)
                    changedItem.minMaxValueSlider.x = prev.minMaxValueSlider.x;
                else {
                    prev.minMaxValueSlider.y = changedItem.minMaxValueSlider.x;
                }
            }

            if (_difficulties.HasNext(changedItem)) {
                var next = _difficulties.GetNext(changedItem); 
                if (next.minMaxValueSlider.y < changedItem.minMaxValueSlider.y)
                    changedItem.minMaxValueSlider.y = next.minMaxValueSlider.y;
                else {
                    next.minMaxValueSlider.x = changedItem.minMaxValueSlider.y;
                }
            }
            
            changedItem.wasChanged = false;

            _difficulties.First().minMaxValueSlider.x = 0;
            _difficulties.Last().minMaxValueSlider.y = 100;
            
        }

        public void Validate(SelfValidationResult result) {
            if (_difficulties.Count == 0)
            {
                result.AddError($"'{nameof(_difficulties)}' is empty list!").WithFix(() => _difficulties.Add(new DifficultyDistribution
                {
                    minMaxValueSlider = new Vector2(0,100)
                }));
            }
        }
    }
    }
