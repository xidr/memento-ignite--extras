using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace MIE.Editor {
    public class HelpfulButtons : OdinEditorWindow {
        [MenuItem("XTools/Helpful Buttons")]
        static void OpenWindow() {
            GetWindow<HelpfulButtons>().Show();
        }

        [HorizontalGroup("row1")]
        [Button(ButtonSizes.Large)]
        void MainMenuScene() {
            LoadScene(MIEConstants.MAIN_MENU_SCENE_PATH);
        }

        [HorizontalGroup("row1")]
        [Button(ButtonSizes.Large)]
        void GameplayScene() {
            LoadScene(MIEConstants.GAMEPLAY_SCENE_PATH);;
        }
        
        [HorizontalGroup("row1")]
        [Button(ButtonSizes.Large)]
        void CoreScene() {
            LoadScene(MIEConstants.CORE_SCENE_PATH);
        }

        void LoadScene(string scenePath) {
            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                EditorSceneManager.OpenScene(scenePath);
        }
    }
}