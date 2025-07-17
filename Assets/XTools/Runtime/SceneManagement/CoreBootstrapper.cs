using UnityEngine;
using UnityEngine.SceneManagement;

namespace XTools {
    internal class CoreBootstrapper {
        // [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Init() {
            Debug.Log("Core Bootstrapper Init");
            SceneManager.LoadScene(SceneLoader.CORE_SCENE_NAME, LoadSceneMode.Additive);
        }
    }
}