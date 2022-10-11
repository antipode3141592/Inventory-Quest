using PixelCrushers.DialogueSystem;
using PixelCrushers.QuestMachine;
using UnityEngine;

namespace Data.Characters
{
    public class LocationCharacter : MonoBehaviour
    {
        QuestGiver _questGiver;
        DialogueActor _dialogueActor;

        public string questGiverId => _questGiver.id.text;

        void Awake()
        {
            _questGiver = GetComponent<QuestGiver>();
            _dialogueActor = GetComponent<DialogueActor>();
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