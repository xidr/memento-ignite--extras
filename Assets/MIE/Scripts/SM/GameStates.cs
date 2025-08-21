using System;
using XTools.SM.Silver;

namespace MIE.Silver {
    [Serializable]
    public abstract class PlayerStateBase : State<object, PlayerStateBase> { }
    
    [Serializable]
    public class PlayerRootState : PlayerStateBase { }
    
    
    [Serializable]
    public class IdleState : PlayerStateBase { }


    [Serializable]
    public abstract class GameStateBase : State<object, GameStateBase> { }


    [Serializable]
    public class GameRootState : GameStateBase { }

    // --- Main Menu States ---

    [Serializable]
    public class MainMenuState : GameStateBase { }

    [Serializable]
    public class MainViewState : GameStateBase { }

    [Serializable]
    public class SettingsViewState : GameStateBase { }

    // --- Gameplay States ---

    [Serializable]
    public class GameplayState : GameStateBase { }
}