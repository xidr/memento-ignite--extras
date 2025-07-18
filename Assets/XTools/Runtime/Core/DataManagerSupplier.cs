using System;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.Audio;

namespace XTools {
    public class DataManagerSupplier : MonoBehaviour {
        [Inject] DataManagerBase _dataManagerBase; 
        
        void Awake() {
            _dataManagerBase.Initialize();
        }
    }
}