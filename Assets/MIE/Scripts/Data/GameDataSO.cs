using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.Localization;

namespace MIE {
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "MIE/GameDataSO", order = 0)]
    public partial class GameDataSO : SerializedScriptableObject {
        // [ReadOnly]
        // [NonSerialized]
        // [ReadOnly]
        [ListDrawerSettings(IsReadOnly = true)]
        public List<LocalizedAsset<TextAsset>> wordsTextAssets = new();
        // [OdinSerialize]
        // [ListDrawerSettings(ShowPaging = false)]
        [ListDrawerSettings(CustomAddFunction = "CustomAddFunction")]
        [HideReferenceObjectPicker]
        public readonly DifficultyLevel[] difficultyLevels = Array.Empty<DifficultyLevel>();


        GameDataSO() {
            for (int i = 0; i < MIEConstants.WORDS_BUNDLES_COUNT; i++) {
                wordsTextAssets.Add(new LocalizedAsset<TextAsset>());
            }
        }


        DifficultyLevel CustomAddFunction()
        {
            return new DifficultyLevel();
        }


        // // Synchronous method - use if the asset is already loaded
        // public void FillWordsContainers() {
        //     for (var i = 0; i < wordsTextAssets.Length; i++) {
        //         var textAsset = wordsTextAssets[i].LoadAsset();
        //         if (textAsset == null) Debug.LogError("TextAsset is null");
        //         var words = textAsset.text.Split(new[] { "\r\n", "\r", "\n" },
        //             StringSplitOptions.RemoveEmptyEntries);
        //         // wordsContainers[i] = new WordsContainer(words);
        //     }
        // }
    }
}