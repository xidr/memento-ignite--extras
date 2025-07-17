using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MIE {
    // [AlchemySerialize]
    public class DataManagerNeo {
        // [AlchemySerializeField, NonSerialized]
        List<ScriptableObject> _data = new();
        
        public DataManagerNeo() {
            // Addressables.LoadAssetsAsync<ScriptableObject>("GameData", SO => {
            //     _data.Add(SO);
            //     Debug.Log(SO.name);
            // });
            ScriptableObject[] loaded = Resources.LoadAll<ScriptableObject>("GameData");

            _data.AddRange(loaded);
        }
        
        public T GetData<T>() where T : GameDataBase {
            ScriptableObject foundData = _data.FirstOrDefault(x => x.GetType() == typeof(T));
            
            T foundDataT = foundData as T;
            if (foundDataT == null) {
                Debug.LogError(typeof(T).Name + " could not be loaded");
            }
            
            return foundDataT;
        } 


    }
}