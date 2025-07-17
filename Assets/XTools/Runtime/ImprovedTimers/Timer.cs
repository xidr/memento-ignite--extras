using System;
using UnityEngine;

namespace XTools {

    public class CountdownTimer : Timer {
        public CountdownTimer(float value) : base(value) {
            
        }

        public override void Tick() {
            if (isRunning && currentTime > 0) {
                currentTime -= Time.deltaTime;
            }

            if (isRunning && currentTime <= 0) {
                Stop();
            }
        }

        public override bool IsFinished => currentTime <= 0;
    }
    
    public abstract class Timer : IDisposable {

        
        public float currentTime { get; protected set; }
        public bool isRunning { get; private set; }

        protected float _initialTime;
        
        public float progress => Mathf.Clamp(currentTime / _initialTime, 0f, 1f);
        
        public Action OnTimerStart = delegate { };
        public Action OnTimerStop = delegate { };

        protected Timer(float value) {
            _initialTime = value;
        }

        public void Start() {
            currentTime = _initialTime;
            if (!isRunning) {
                isRunning = true;
                TimerManager.RegisterTimer(this);
                OnTimerStart.Invoke();
            }
        }

        public void Stop() {
            if (isRunning) {
                isRunning = false;
                TimerManager.DeregisterTimer(this);
                OnTimerStop.Invoke();
            }
        }
        
        public abstract void Tick();
        public abstract bool IsFinished { get; }
        
        public void Resume() => isRunning = true;
        public void Pause() => isRunning = false;
        
        public virtual void Reset() => currentTime = _initialTime;

        public virtual void Reset(float newTime) {
            _initialTime = newTime;
            Reset();
        }
        
        
        bool _disposed = false;
        
        // --- Destruction Logic ---
        
        ~Timer() {
            Dispose(false);
        }
        
        // Call Dispose to ensure deregistration of the timer from the TimerManager
        // when the consumer is done with the timer or being destroyed
        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (_disposed) return;

            // We are coming here from the Disposing method
            if (disposing) {
                TimerManager.DeregisterTimer(this);
            }
            
            _disposed = true;
        }
    }
    
    
}