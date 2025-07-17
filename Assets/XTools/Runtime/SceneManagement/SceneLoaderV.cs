using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace XTools {
    internal class SceneLoaderV : MonoBehaviour {
        static readonly int InHash = Animator.StringToHash("In");
        static readonly int OutHash = Animator.StringToHash("Out");

        [SerializeField] Animator _animator;
        [SerializeField] GameObject _camera;
        public GameObject cameraRef => _camera;
        public bool inProgress { get; private set; }

        public void SetAnim(LoadingAnims anim) {
            inProgress = true;
            switch (anim) {
                case LoadingAnims.In: {
                    _animator.SetTrigger(InHash);
                    break;
                }
                case LoadingAnims.Out: {
                    _animator.SetTrigger(OutHash);
                    break;
                }
            }
        }

        public void SetAnimEnded(LoadingAnims type) {
            _camera.SetActive(type == LoadingAnims.In);
            inProgress = false;
        }

        public enum LoadingAnims {
            In,
            Out
        }
    }
}