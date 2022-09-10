using Data.Characters;
using System;

namespace Data.Quests
{
    public class EscortQuest : Quest
    {
        public EscortQuest(IEscortQuestStats stats) : base(stats)
        {
            EscortCharacterId = stats.EscortCharacterId;
            TargetLocationId = stats.TargetLocationId;
        }

        public string EscortCharacterId { get; }

        public string TargetLocationId { get; }

        public override bool Evaluate(Party party)
        {
            throw new NotImplementedException();
        }
    }
}
