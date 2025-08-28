using UnityEngine;

namespace MIE {
    public class GameplayManager : MonoBehaviour, IValidatableSerializedFields {

        [SerializeField] WordsController _wordsController;
        [SerializeField] TypingController _typingController;


    }
}