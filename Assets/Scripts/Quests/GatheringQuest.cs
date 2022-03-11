using Data;
using Data.Interfaces;
using System;

namespace InventoryQuest.Quests
{
    public class GatheringQuest : IQuest
    {
        

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string TargetItemId { get; }

        public int TargetQuantity { get; }

        public string RewardId { get; }

        public GatheringQuest(GatheringQuestStats stats)
        {
            Id = Guid.NewGuid().ToString();
            Name = stats.Name;
            Description = stats.Description;
            TargetItemId = stats.TargetItemId;
            TargetQuantity = stats.TargetQuantity;
            RewardId = stats.RewardId;
        }

        public bool Evaluate(Party party)
        {
            int targetCount = party.GetPartyItemCountById(TargetItemId);
            if (targetCount == TargetQuantity) return true;
            return false;
        }

        public void Cancel()
        {

        }
    }
}
