using Data.Characters;
using System;

namespace Data.Quests
{
    public class BountyQuest : Quest
    {
        public BountyQuest(IBountyQuestStats stats) : base(stats)
        {
            BountyTargetQuantity = stats.BountyTargetQuantity;
            BountyTargetId = stats.BountyTargetId;
        }
        public int CurrentQuantity { get; set; }

        public int BountyTargetQuantity { get; }

        public string BountyTargetId { get; }

        public override bool Evaluate(Party party)
        {
            throw new NotImplementedException();
        }

        public override void Process(Party party)
        {
            throw new System.NotImplementedException();
        }
    }
}
