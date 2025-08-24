using System;
using System.Collections.Generic;
using UnityEngine;
using XTools;
using static XTools.XToolsEvents;
using XTools.SM.Silver;

namespace MIE {
    [RequireComponent(typeof(StateMachine))]
    public class SceneManager : MonoBehaviour {
        // [SerializeReference] public List<SceneStateBase> states = new();
        
        StateMachine _stateMachine;

        void Awake() {
            _stateMachine = GetComponent<StateMachine>();
        }


        void Update() {
            _stateMachine.Tick(Time.deltaTime);
        }


    }
}