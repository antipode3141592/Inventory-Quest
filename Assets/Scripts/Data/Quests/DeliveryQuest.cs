using Data.Characters;
using System;
using System.Collections.Generic;

namespace Data.Quests
{
    public class DeliveryQuest : Quest
    {
        public DeliveryQuest(DeliveryQuestStats stats) : base(stats)
        {
            DeliveryItemIdsAndQuantities = stats.DeliveryItemIdsAndQuantities;
        }

        public IList<(string,int)> DeliveryItemIdsAndQuantities { get; }

        public override bool Evaluate(Party party)
        {
            throw new NotImplementedException();
        }
    }
}
