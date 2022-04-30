using Data.Characters;
using System;

namespace Data.Quests
{
    public class BountyQuest : Quest
    {
        public BountyQuest(BountyQuestStats stats) : base(stats)
        {
            BountyTargetQuantity = stats.BountyTargetQuantity;
            BountyTargetId = stats.BountyTargetId;
            BountyTargetType = stats.BountyTargetType;
        }
        public int CurrentQuantity { get; set; }

        public int BountyTargetQuantity { get; }

        public string BountyTargetId { get; }

        public Type BountyTargetType { get; }

        public override bool Evaluate(Party party)
        {
            throw new NotImplementedException();
        }
    }
}
