using UnityEditor;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.PlayerLoop;

namespace XTools {
    internal static class TimeBootstrapper {
        static PlayerLoopSystem timerSystem;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        internal static void Initialize() {
            PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();

            if (!InsertTimerManager<Update>(ref currentPlayerLoop, 0)) {
                Debug.LogWarning(
                    "Improved Timers not initialized, unable to register TimerManager into the Update loop.");
                return;
            }

            // Set it as the graph that Unity is actually gonna use
            PlayerLoop.SetPlayerLoop(currentPlayerLoop);

            // Peek into the Unity Player Loop
            PlayerLoopUtils.PrintPlayerLoop(currentPlayerLoop);

#if UNITY_EDITOR

            EditorApplication.playModeStateChanged -= OnPlayModeState;
            EditorApplication.playModeStateChanged += OnPlayModeState;


            static void OnPlayModeState(PlayModeStateChange state) {
                if (state == PlayModeStateChange.EnteredEditMode) {
                    PlayerLoopSystem currentPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
                    RemoveTimerManager<Update>(ref currentPlayerLoop);
                    PlayerLoop.SetPlayerLoop(currentPlayerLoop);
                    
                    TimerManager.Clear();
                }
            }
            
#endif
            
        }

        static void RemoveTimerManager<T>(ref PlayerLoopSystem loop) {
            PlayerLoopUtils.RemoveSystem<T>(ref loop, in timerSystem);
        }

        // So we can add our system to any sub system, not just to Update
        static bool InsertTimerManager<T>(ref PlayerLoopSystem loop, int index) {
            timerSystem = new PlayerLoopSystem() {
                type = typeof(TimerManager),
                // Which method is gonna be called inside of that class
                updateDelegate = TimerManager.UpdateTimers,
                subSystemList = null
            };

            // Actually insert it
            return PlayerLoopUtils.InsertSystem<T>(ref loop, in timerSystem, index);
        }
    }
}