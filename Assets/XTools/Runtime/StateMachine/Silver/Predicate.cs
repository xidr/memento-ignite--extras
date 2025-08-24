using System;
using Sirenix.Serialization;
using UnityEngine;

namespace XTools.SM.Silver {
    public interface IPredicate {
        bool Evaluate();
    }

    /// <summary>
    ///     Represents a predicate that uses a Func delegate to evaluate a condition.
    /// </summary>
    public class FuncPredicate : IPredicate {
        readonly Func<bool> func;

        public FuncPredicate(Func<bool> func) {
            this.func = func;
        }

        public bool Evaluate() => func.Invoke();
    }

    /// <summary>
    ///     Represents a predicate that encapsulates an action and evaluates to true once the action has been invoked.
    /// </summary>
    public class ActionPredicate : IPredicate {
        public bool flag;

        public ActionPredicate(ref Action eventReaction) {
            eventReaction += () => { flag = true; };
        }

        public bool Evaluate() {
            var result = flag;
            flag = false;
            return result;
        }
    }

    public class ActionPredicateWithData<T> : IPredicate {
        public bool flag;

        public ActionPredicateWithData(ref Action<T> eventReaction) {
            eventReaction += T => { flag = true; };
        }

        public bool Evaluate() {
            var result = flag;
            flag = false;
            return result;
        }
    }

    public class ActionPredicateCompare<T> : IPredicate {
        bool _flag;
        T _referenceObject;

        public ActionPredicateCompare(ref Action<T> eventReaction, T referenceObject) {
            _referenceObject = referenceObject;
            eventReaction += delegate(T compareObject) { _flag = _referenceObject.Equals(compareObject); };
        }

        public bool Evaluate() {
            var result = _flag;
            _flag = false;
            return result;
        }
    }

    public class ActionPredicateUIButtonPressed : IPredicate {
        [SerializeField]
        XToolsEvents.UIButtonTypes _referenceObject;
        
        bool _flag;

        public ActionPredicateUIButtonPressed() {
            XToolsEvents.UIButtonPressed += delegate(XToolsEvents.UIButtonTypes compareObject) {
                _flag = _referenceObject.Equals(compareObject);
            };
        }

        public bool Evaluate() {
            var result = _flag;
            _flag = false;
            return result;
        }
    }
}