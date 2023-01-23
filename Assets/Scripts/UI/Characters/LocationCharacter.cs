using PixelCrushers.DialogueSystem;
using PixelCrushers.QuestMachine;
using UnityEngine;
using TMPro;

namespace Data.Characters
{
    public class LocationCharacter : MonoBehaviour
    {
        QuestGiver _questGiver;
        DialogueActor _dialogueActor;

        [SerializeField] TextMeshProUGUI nameText;

        public string questGiverId => _questGiver.id.text;

        void Awake()
        {
            _questGiver = GetComponent<QuestGiver>();
            _dialogueActor = GetComponent<DialogueActor>();
        }

        void Start()
        {
            nameText.text = _questGiver.id.text;
        }

        public void Chat()
        {
            DialogueManager.StartConversation(_dialogueActor.actor);
        }

        void OnMouseUpAsButton()
        {
            Chat();
        }
    }
}