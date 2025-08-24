using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace XTools.SM.Silver {
    [Serializable]
    public class TransitionData {
        public IState to;
        public IPredicate predicate;
    }


    // public abstract class Transition {
    //     public IState to { get; protected set; }
    //     public abstract bool Evaluate();
    // }

    [Serializable]
    public class TestT {
        
    }
    
    [Serializable]
    public class Transition {
        // [OdinSerialize, ValueDropdown("GetObjectOptions")]
        // IState _from;
        // public IState from => _from; 
        //
        // [OdinSerialize, ValueDropdown("GetObjectOptions")]
        // IState _to;
        // public IState to => _to;  
        
        [OdinSerialize]
        public readonly IPredicate condition;

        [HideInInspector, OdinSerialize]
        IState _parentState;
        
        // public Transition(IState to, IPredicate condition) {
        //     _to = to;
        //     this.condition = condition;
        // }

        public bool Evaluate() {
            // Check if the condition variable is a Func<bool> and call the Invoke method if it is not null
            // var result = (condition as Func<bool>)?.Invoke();
            // if (result.HasValue) {
            //     return result.Value;
            // }

            // // Check if the condition variable is an ActionPredicate and call the Evaluate method if it is not null
            // result = (condition as ActionPredicate)?.Evaluate();
            // if (result.HasValue) {
            //     return result.Value;
            // }

            // Check if the condition variable is an IPredicate and call the Evaluate method if it is not null
            // if (_parentState.activeChild != _from) return false;
            
            var result = condition?.Evaluate();
            if (result.HasValue) return result.Value;

            // If the condition variable is not a Func<bool>, an ActionPredicate, or an IPredicate, return false
            return false;
        }
        
        internal void SetParent(IState parent) {
            _parentState = parent;
        }
        
        IEnumerable<ValueDropdownItem<IState>> GetObjectOptions() {
            
            return _parentState.GetChildren()
                .Where(obj => obj != null)
                .Select(obj => new ValueDropdownItem<IState>(obj.ToString(), obj));
        }
    }
}