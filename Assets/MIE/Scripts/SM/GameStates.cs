using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using XTools.SM.Silver;

namespace MIE.Silver {
    [Serializable]
    public abstract class GameStateBase : State {
        [OdinSerialize] public List<GameStateBase> children = new();

    }


    [Serializable]
    public class GameRootState : GameStateBase {
    }
    
    // --- Main Menu States ---

    [Serializable]
    public class MainMenuState : GameStateBase {
        
    }

    [Serializable]
    public class MainViewState : GameStateBase {
    }

    [Serializable]
    public class SettingsViewState : GameStateBase {
    }
    
    // --- Gameplay States ---
    
    [Serializable]
    public class GameplayState : GameStateBase {
        
    }
}