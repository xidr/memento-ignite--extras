using System;
using UnityEngine;

namespace XTools {
    public class TimerExample : MonoBehaviour {

        CountdownTimer _timer, _timer2;

        [SerializeField] float _timerDuration = 10f;
        [SerializeField] float _timer2Duration = 10f;

        void Start() {
            _timer = new CountdownTimer(_timerDuration);
            _timer.OnTimerStart += () => Debug.Log("Timer Started");
            _timer.OnTimerStop += () => Debug.Log("Timer Stopped");
            _timer.Start();
            
            _timer2 = new CountdownTimer(_timer2Duration);
            _timer2.OnTimerStart += () => Debug.Log("Timer 2 Started");
            _timer2.OnTimerStop += () => Debug.Log("Timer 2 Stopped");
            _timer2.Start();
        }

        void Update() {
            Debug.Log($"Timer: {_timer.progress}");
            Debug.Log($"Timer2: {_timer2.progress}");
        }

        void OnDestroy() {
            _timer.Dispose();
            _timer2.Dispose();
        }
        
    }
}