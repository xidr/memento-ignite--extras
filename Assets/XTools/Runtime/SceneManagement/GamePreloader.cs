using System;
using UnityEngine;

namespace XTools {
    public class GamePreloader : MonoBehaviour {
        void Start() {
            ServiceLocator.For(this).Get(out SceneLoader sceneLoader);
            sceneLoader.TryLoadScene(sceneLoader.initialSceneName);
        }
    }
}