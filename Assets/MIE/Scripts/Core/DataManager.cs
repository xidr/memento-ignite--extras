using Sirenix.OdinInspector;
using UnityEngine;
using XTools;

namespace MIE {
    public class DataManager : DataManagerBase {
        
        [SerializeField]
        [MinMaxSlider(-10, 10, true)]
        Vector2 WithFields = new Vector2(-3, 4);
        

        
    }
}