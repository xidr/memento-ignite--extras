using System;
using MIE;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Localization;

public class Test : MonoBehaviour {

    public LocalizedAsset<TextAsset> dictionaryTextAsset;

    void Start() {
        Debug.Log(GetLocalizedText());
    }

    // Synchronous method - use if the asset is already loaded
    public string GetLocalizedText() {
        if (dictionaryTextAsset.IsEmpty)
            return string.Empty;
                
        var textAsset = dictionaryTextAsset.LoadAsset();
        return textAsset != null ? textAsset.text : string.Empty;
    }
}