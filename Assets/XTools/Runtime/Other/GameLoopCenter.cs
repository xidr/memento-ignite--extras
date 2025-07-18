using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace XTools {
    public class GameLoopCenter : Singleton<GameLoopCenter> {
        public delegate void UpdateDelegate(float delta);

        HashSet<UpdateDelegate> _updateDelegates = new();
        HashSet<IDisposable> _disposables = new();

        void Update() {
            var delta = Time.deltaTime;
            foreach (var update in _updateDelegates.ToArray()) {
                update?.Invoke(delta);
            }
        }

        public void SubscribeToUpdate(UpdateDelegate updateDelegate) {
            _updateDelegates.Add(updateDelegate);
        }

        public void UnsubscribeFromUpdate(UpdateDelegate updateDelegate) {
            _updateDelegates.Remove(updateDelegate);
        }

        public void SubscribeForDispose(IDisposable disposable) {
            _disposables.Add(disposable);
        }

        public void UnsubscribeFromDispose(IDisposable disposable) {
            _disposables.Remove(disposable);
        }

        void OnDestroy() {
            _updateDelegates.Clear();
            foreach (var d in _disposables) {
                d.Dispose();
            }

            _disposables.Clear();
        }
    }
}