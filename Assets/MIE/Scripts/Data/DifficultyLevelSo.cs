using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Localization;

namespace MIE {
    public class DifficultyLevelSo : ScriptableObject {
        public LocalizedAsset<TextAsset> dictionaryTextAsset;

        
        // Synchronous method - use if the asset is already loaded
        public string GetLocalizedText() {
            if (dictionaryTextAsset.IsEmpty)
                return string.Empty;
                
            var textAsset = dictionaryTextAsset.LoadAsset();
            return textAsset != null ? textAsset.text : string.Empty;
        }




    }
}