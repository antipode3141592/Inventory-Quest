using Data.Characters;
using Data.Quests;
using System;

namespace InventoryQuest.Quests
{
    public class DeliveryQuest : IQuest
    {
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string RewardId { get; }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public bool Evaluate(Party party)
        {
            throw new NotImplementedException();
        }
    }
}
