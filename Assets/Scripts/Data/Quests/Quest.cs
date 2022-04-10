using Data.Characters;
using System;

namespace Data.Quests
{
    public abstract class Quest : IQuest
    {
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string RewardId { get; }

        public virtual void Cancel()
        {
            throw new NotImplementedException();
        }

        public virtual bool Evaluate(Party party)
        {
            throw new NotImplementedException();
        }
    }
}
