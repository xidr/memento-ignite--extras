using System;
using Sirenix.Serialization;
using XTools.SM.Silver;

namespace MIE.Silver {
    public class MIEStateMachine : StateMachine {
        // public MIEStateMachine(State root) : base(root) { }
        
        [NonSerialized, OdinSerialize]
        public GameStateBase root2;
        
        [NonSerialized, OdinSerialize]
        public TestSerialization test3;
    }
}