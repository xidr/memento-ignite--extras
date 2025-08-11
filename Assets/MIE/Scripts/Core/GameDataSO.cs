using Sirenix.OdinInspector;
using UnityEngine;

namespace MIE {
    [CreateAssetMenu(fileName = "GameDataSO", menuName = "MIE/GameDataSO", order = 0)]
    public class GameDataSO : SerializedScriptableObject{
        [SerializeField]
        [MinMaxSlider(-10, 10, true)]
        Vector2 WithFields = new Vector2(-3, 4);
        
        [ValidateInput("MustBeNull", "This field should be null.")]
        [ValidateInput("@_range1.y < _range2.x", "Check", InfoMessageType.Warning)]
        [SerializeField]
        // [MinMaxSlider(0, "@_range2.x", true)]
        Vector2 _range1 = new Vector2(0, 450);
        
        [SerializeField]
        // [MinMaxSlider("@_range1.y", "@_range3.x", true)]
        [MinMaxSlider(0, 450, true)]
        Vector2 _range2 = new Vector2(0, 450);
        
        [OnValueChanged()]
        [SerializeField]
        [MinMaxSlider("@_range2.y", 450, true)]
        Vector2 _range3 = new Vector2(0, 450);
        
        
        
        
    }
}