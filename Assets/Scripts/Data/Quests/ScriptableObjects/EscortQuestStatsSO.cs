using UnityEngine;

namespace Data.Quests
{
    [CreateAssetMenu(menuName = "InventoryQuest/QuestStats/Escort", fileName = "Escort_")]
    public class EscortQuestStatsSO : QuestStatsSO, IEscortQuestStats
    {
        [SerializeField] protected string escortCharacterId;
        [SerializeField] protected string targetLocationId;

        public string EscortCharacterId => escortCharacterId; 
        public string TargetLocationId => targetLocationId;
    }
}
