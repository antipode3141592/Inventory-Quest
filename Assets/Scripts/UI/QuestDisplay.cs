using Data.Quests;
using InventoryQuest.Managers;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class QuestDisplay : MonoBehaviour
    {
        QuestManager _questManager;

        IQuest selectedQuest;

        [Inject]
        public void Init(QuestManager questManager)
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
