using System;

namespace XTools {
    public static class XToolsEvents {
        
        public static Action<UIButtonTypes> UIButtonPressed = delegate(UIButtonTypes _) {}; 
        public enum UIButtonTypes {
            Back,
            Pause,
            Settings,
            HowToPlay,
            ToMainMenu,
            ToGameplay,
        }
        
    }
}