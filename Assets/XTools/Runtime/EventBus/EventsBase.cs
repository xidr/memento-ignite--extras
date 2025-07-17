namespace XTools {
    public interface IEvent {
    }
    
    public struct DataChanged : IEvent {}

    public struct UIAudioSliderChanged : IEvent {
        public UIAudioSliders audioSliderType;
        public float value;
        
        public enum UIAudioSliders {
            SfxVolume,
            MusicVolume,
        }
    }
    
    public struct UIButtonPressed : IEvent {
        public UIButtons buttonType;

        public enum UIButtons {
            Back,
            Pause,
            LoadMainMenu,
            LoadGameplay,
            Settings
        }
    }
    
    



    // And he uses struct bcs:
    // "Structs are allocated on a stack, not a heap so they put way less pressure on the garbage collector"
    // Pretty cool
}