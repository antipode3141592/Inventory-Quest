using System;

namespace Data.Characters
{
    public abstract class SkillStat : IStat
    {
        protected SkillStat(int initialValue)
        {
            InitialValue = initialValue;
        }

        public virtual Type Type { get; }
        public virtual StatTypes Id { get; }

        public int InitialValue { get; }

        public int PurchasedLevels { get; set; }

        public int CurrentValue => InitialValue + PurchasedLevels + Modifier;

        public int Modifier { get; set; }
    }
}
