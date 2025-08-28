using Reflex.Attributes;
using UnityEngine;

namespace MIE {
    public class WordsController : MonoBehaviour {

        [Inject] DataManager _dataManager;

        WordsModel _model;

        void Init() {
            var data = _dataManager.GetData<GameDataSO>();
            _model = WordsModel.CreateAndGetData(data);
        }

    }

}