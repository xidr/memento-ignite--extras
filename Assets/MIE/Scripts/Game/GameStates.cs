using System;
using System.Collections.Generic;
using UnityEngine;
using XTools.SM.Silver;

namespace MIE {
    [Serializable]
    public abstract class GameStateBase : State<SceneManager, GameStateBase> {
        [SerializeField] List<GameObject> _views = new();

        protected override void OnEnter() {
            base.OnEnter();
            _views.ForEach(view => view.SetActive(true));
            _context.Check(GetType().ToString());
        }

        protected override void OnExit() {
            base.OnExit();
            _views.ForEach(view => view.SetActive(false));
        }
    }


    [Serializable]
    public class GameRootState : State<SceneManager, GameStateBase>, IRootState { }

    // --- Main Menu States ---

    [Serializable]
    public class MainMenuState : GameStateBase { }

    [Serializable]
    public class MainViewState : GameStateBase { }

    [Serializable]
    public class SettingsViewState : GameStateBase { }

    // --- Gameplay States ---

    [Serializable]
    public class GameplayState : GameStateBase { }
}