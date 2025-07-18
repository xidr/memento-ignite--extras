using System;
using Reflex.Attributes;
using UnityEngine;

namespace XTools {
    public class GamePreloader : MonoBehaviour {
        [SerializeField] string _initialSceneName = "MainMenu";
        
        [Inject] SceneLoader _sceneLoader;
        
        void Start() {
            _sceneLoader.TryLoadScene(_initialSceneName);
        }
    }
}