using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.Quests
{
    [CreateAssetMenu(menuName = "InventoryQuest/QuestStats/Bounty", fileName = "BountyQuestSO_")]
    public class BountyQuestStatsSO : QuestStatsSO, IBountyQuestStats
    {
        [SerializeField] protected int bountyTargetQuantity;
        [SerializeField] protected string bountyTargetId;

        public int BountyTargetQuantity => bountyTargetQuantity;
        public string BountyTargetId => bountyTargetId;
    }
}
