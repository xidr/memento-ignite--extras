using System;
using System.Collections.Generic;
using UnityEngine;

namespace XTools {
    public static class EventBus <T> where T : IEvent {
        static readonly HashSet<IEventBinding<T>> bindings = new();
        static readonly Dictionary<EventBinding<T>, bool> bindingsToProcess = new();

        public static void Register(EventBinding<T> binding) {
            if (_isRaisingEvent)
                bindingsToProcess.Add(binding, true);
            else
                bindings.Add(binding);
        }

        public static void Deregister(EventBinding<T> binding) {
            if (_isRaisingEvent)
                bindingsToProcess.Add(binding, false);
            else
                bindings.Remove(binding);
        }

        static bool _isRaisingEvent = false;

        // @ is pronounced as "at"
        // But why does he call both events?
        public static void Raise(T @event) {
            _isRaisingEvent = true;
            foreach (var binding in bindings) {
                if (bindingsToProcess.ContainsKey(binding as EventBinding<T>)) continue;
                binding.OnEvent.Invoke(@event);
                binding.OnEventNoArgs.Invoke();
            }
            _isRaisingEvent = false;
            DelayedProcess();
        }

        static void DelayedProcess() {
            foreach (var keyValuePair in bindingsToProcess)
                if (keyValuePair.Value)
                    Register(keyValuePair.Key);
                else
                    Deregister(keyValuePair.Key);
            bindingsToProcess.Clear();
        }

        static void Clear() {
            Debug.Log($"Clearing {typeof(T).Name} bindings");
            bindings.Clear();
        }
    }
}