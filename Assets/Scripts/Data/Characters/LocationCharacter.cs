using PixelCrushers.QuestMachine;
using PixelCrushers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

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
            //Debug.Log($"Start Chatting!");
            //if (!_questGiver.HasOfferableOrActiveQuest()) 
            //{
            //    Debug.Log($"No Quests available!");
            //    var conversations = DialogueManager.DatabaseManager.MasterDatabase.conversations.FindAll(x => x.ActorID. == _dialogueActor.actor);
            DialogueManager.StartConversation(_dialogueActor.actor);
            //    return;
            //}
            //Debug.Log($"Quests available!");
            //_questGiver.StartDialogueWithPlayer();
        }

        void OnMouseUpAsButton()
        {
            Chat();
        }
    }
}