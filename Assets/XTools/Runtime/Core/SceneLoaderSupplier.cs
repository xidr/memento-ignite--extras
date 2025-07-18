using System;
using Reflex.Attributes;
using UnityEngine;

namespace XTools {
    public class SceneLoaderSupplier : MonoBehaviour {
        [Inject] SceneLoader _sceneloader;
        
        [SerializeField] SceneLoaderV _view;

        void Awake() {
            _sceneloader.Initialize(_view);
        }
    }
}