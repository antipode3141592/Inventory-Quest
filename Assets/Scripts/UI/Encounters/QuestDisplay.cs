using Data.Quests;
using InventoryQuest.Managers;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class QuestDisplay : MonoBehaviour
    {
        IQuestManager _questManager;

        IQuest selectedQuest;

        [Inject]
        public void Init(IQuestManager questManager)
        {
            _questManager = questManager;
        }

        private void Awake()
        {
            
        }

        public void AcceptQuest()
        {

        }


    }
}
