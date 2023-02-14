using PixelCrushers.DialogueSystem;
using PixelCrushers.QuestMachine;
using UnityEngine;
using TMPro;
using InventoryQuest.UI;

namespace Data.Characters
{
    public class LocationCharacter : MonoBehaviour, IEnableable
    {
        QuestGiver _questGiver;
        DialogueActor _dialogueActor;

        [SerializeField] TextMeshProUGUI nameText;
        bool isAvailable;

        public string questGiverId => _questGiver.id.text;

        void Awake()
        {
            _questGiver = GetComponent<QuestGiver>();
            _dialogueActor = GetComponent<DialogueActor>();
        }

        void Start()
        {
            nameText.text = _questGiver.id.text;
            Enable();
        }

        public void Chat()
        {
            DialogueManager.StartConversation(_dialogueActor.actor);
        }

        void OnMouseUpAsButton()
        {
            if (!isAvailable)
                return;
            Chat();
        }

        public void Disable()
        {
            isAvailable = false;
        }

        public void Enable()
        {
            isAvailable = true;
        }
    }
}