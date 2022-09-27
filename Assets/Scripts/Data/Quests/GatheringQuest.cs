using Data.Characters;

namespace Data.Quests
{
    /// <summary>
    /// A classic collection quest.  Gather [Quantity] [IItems] and take them to the specified location.
    /// </summary>
    public class GatheringQuest : Quest
    {
        public GatheringQuest(IGatheringQuestStats stats) : base(stats)
        {
            TargetItemId = stats.TargetItemId;
            TargetQuantity = stats.TargetQuantity;
        }

        public string TargetItemId { get; }

        public int TargetQuantity { get; }

        public string TargetLocationId { get; }

        public override bool Evaluate(Party party)
        {
            int targetCount = QuestHelpers.GetPartyItemCountById(party, TargetItemId);
            if (targetCount == TargetQuantity) return true;
            return false;
        }

        public override void Process(Party party)
        {
            throw new System.NotImplementedException();
        }
    }
}
