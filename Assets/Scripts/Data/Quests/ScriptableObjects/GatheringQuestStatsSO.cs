using UnityEngine;

namespace Data.Quests
{
    [CreateAssetMenu(menuName = "InventoryQuest/QuestStats/Gathering", fileName = "Gathering_")]
    public class GatheringQuestStatsSO : QuestStatsSO, IGatheringQuestStats
    {
        [SerializeField] protected string targetItemId;
        [SerializeField] protected int targetQuantity;

        public string TargetItemId => targetItemId;

        public int TargetQuantity => targetQuantity;
    }
}
