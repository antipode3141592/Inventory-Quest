using PixelCrushers.QuestMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Characters
{
    public class LocationCharacter : MonoBehaviour
    {
        QuestGiver _questGiver;

        public string questGiverId => _questGiver.id.text;

        void Awake()
        {
            _questGiver = GetComponent<QuestGiver>();
        }

        public void Chat()
        {
            Debug.Log($"Start Chatting!");
            //if (!_questGiver.HasOfferableOrActiveQuest()) return;
            //Debug.Log($"Quests available!");
            _questGiver.StartDialogueWithPlayer();
        }

        private void OnMouseUpAsButton()
        {
            Chat();
        }
    }
}