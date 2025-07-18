using System;
using System.Collections.Generic;
using UnityEngine;
using XTools.SM.Iron;

namespace MIE {

    [Serializable]
    public abstract class SceneStateBase : IState {

        public List<GameObject> views;

        public void OnEnter() {
            views.ForEach(view => view.SetActive (true));
        }

        public void OnExit() {
            views.ForEach(view => view.SetActive (false));
        }
    }

    [Serializable]
    public sealed class MainViewState : SceneStateBase {

    }

    [Serializable]
    public sealed class SettingsState : SceneStateBase {

    }
    
}