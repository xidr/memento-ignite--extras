using System;
using System.Collections;
using Reflex.Core;
using Reflex.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XTools {
    public class SceneLoader {
        internal const string CORE_SCENE_NAME = "Core";

        SceneLoaderV _view;
        
        readonly SceneLoaderM _model = new();

        bool _isLoading;


        internal void Initialize(SceneLoaderV view) {
            _view = view;
        }
        
        public void TryLoadScene(string sceneName, bool withAnims = false) {
            if (_isLoading) return;

            GameLoopCenter.Instance.StartCoroutine(LoadSceneAsync(sceneName, withAnims));
        }

        IEnumerator LoadSceneAsync(string sceneName, bool withAnims = false) {
            _isLoading = true;

            if (withAnims) {
                _view.SetAnim(SceneLoaderV.LoadingAnims.In);
                yield return new WaitUntil(() => !_view.inProgress);
            }

            _view.cameraRef.SetActive(true);
            
            GameLoopCenter.Instance.StartCoroutine(_model.LoadScene(sceneName));

            // yield return modelCor;
            yield return new WaitUntil(() => !_model.inProgress);
            // yield return new WaitForSeconds(0.2f);
            // var coreScene = gameObject.scene;
            // var activeScene = SceneManager.GetSceneByName(sceneName);
            //
            // ReflexSceneManager.OverrideSceneParentContainer(scene: activeScene, parent: coreScene.GetSceneContainer());

            if (withAnims) _view.SetAnim(SceneLoaderV.LoadingAnims.Out);

            _view.cameraRef.SetActive(false);
            
            _isLoading = false;
        }

    }
}