using Data.Interfaces;
using System;

namespace InventoryQuest
{
    public class GatheringQuest : IQuest
    {
        Party _party;

        public GatheringQuest(string name, string description, string targetItemId, int targetQuantity, string rewardId, Party party)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Description = description;
            TargetItemId = targetItemId;
            TargetQuantity = targetQuantity;
            RewardId = rewardId;
            _party = party;
        }

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string TargetItemId { get; }

        public int TargetQuantity { get; }

        public string RewardId { get; }

        public bool Evaluate()
        {
            int targetCount = _party.CountItemInParty(TargetItemId);
            if (targetCount == TargetQuantity) return true;
            return false;
        }

        public void Cancel()
        {

        }
    }
}
