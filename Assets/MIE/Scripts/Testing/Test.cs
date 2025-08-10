// using MIE;
// using Reflex.Attributes;
// using UnityEngine;
//
// public class Test : MonoBehaviour {
//
//     [Inject] DataManager _manager;
//     
//     void Start() {
//         var a = _manager.GetData<TestData>();
//         Debug.Log(a.testInt);
//             
//         var b = _manager.GetData<TestData2>();
//         Debug.Log(b.string1);
//     }
// }