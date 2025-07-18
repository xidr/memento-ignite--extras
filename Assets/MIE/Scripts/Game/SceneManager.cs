using System;
using System.Collections.Generic;
using Alchemy.Serialization;
using UnityEngine;
using XTools;
using static XTools.XToolsEvents;
using XTools.SM.Iron;

namespace MIE.Game {
    public class SceneManager : MonoBehaviour {
        [SerializeReference] public List<SceneStateBase> states = new();

        StateMachine _stateMachine;
        EventBinding<UIButtonPressed> _UIButtonPressedBinding;

        void Start() {
            SetupStateMachine();
        }

        void Update() {
            _stateMachine.Update();
        }

        void SetupStateMachine() {
            _stateMachine = new StateMachine();

            var mainViewState = states.Find(v => v is MainViewState) as MainViewState;
            var settingsState = states.Find(v => v is SettingsState) as SettingsState;

            At(mainViewState, settingsState,
                new ActionPredicateCompare<UIButtonTypes>(ref XToolsEvents.UIButtonPressed, UIButtonTypes.Settings));

            _stateMachine.SetState(mainViewState);
        }

        void At(IState from, IState to, ActionPredicateCompare<UIButtonTypes> condition) {
            _stateMachine.AddTransition(from, to, condition);
        }
    }
}