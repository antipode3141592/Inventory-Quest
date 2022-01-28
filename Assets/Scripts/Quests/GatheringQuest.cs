using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace InventoryQuest
{
    public class GatheringQuest : IQuest
    {
        public GatheringQuest(string id, string description, string targetItemId, int targetQuantity, string rewardId)
        {
            Id = id;
            Description = description;
            TargetItemId = targetItemId;
            TargetQuantity = targetQuantity;
            RewardId = rewardId;
        }

        public string Id { get; }

        public string Description { get; }

        public string TargetItemId { get; }

        public int TargetQuantity { get; }

        public string RewardId { get; }

        public bool Evaluate(Party party)
        {
            int targetCount = party.CountItemInParty(TargetItemId);
            if (targetCount == TargetQuantity) return true;
            return false;
        }

        public void Cancel()
        {

        }
    }
}
