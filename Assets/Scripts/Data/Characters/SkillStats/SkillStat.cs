using System;

namespace Data.Characters
{
    public class SkillStat : IStat
    {
        public int InitialValue { get; }

        public int PurchasedLevels { get; set; }

        public int CurrentValue => InitialValue + PurchasedLevels + Modifier;

        public int Modifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
