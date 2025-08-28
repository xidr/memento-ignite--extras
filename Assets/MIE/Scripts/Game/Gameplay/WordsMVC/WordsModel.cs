using System;
using UnityEngine;

namespace MIE {
    public class WordsModel {

        readonly GameDataSO _data;
        readonly string[][] _words = new string[MIEConstants.WORDS_BUNDLES_COUNT][];

        WordsModel(GameDataSO data) {
            _data = data;
        }

        void GetLocalizedText() {
            string[] words;
            TextAsset textAsset;

            for (var i = 0; i < MIEConstants.WORDS_BUNDLES_COUNT; i++) {
                textAsset = _data.wordsTextAssets[i].LoadAsset();
                words = textAsset.text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                _words[i] = words;
            }
        }

        public static WordsModel CreateAndGetData(GameDataSO data) {
            var model = new WordsModel(data);

            model.GetLocalizedText();

            return model;
        }

    }
}