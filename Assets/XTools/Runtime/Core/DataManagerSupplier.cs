using System;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;

namespace XTools {
    public class DataManagerSupplier : MonoBehaviour {
        [SerializeField] AssetLabelReference _dataManagerAsset;
        
        [Inject] DataManagerBase _dataManagerBase; 
        
        void Awake() {
            _dataManagerBase.Initialize();
        }
    }
}