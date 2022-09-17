using System;

namespace Data.Characters
{

    public abstract class CharacterStat : IStat
    {
        public virtual Type Type { get; }
        public virtual CharacterStatTypes Id { get; }

        public int InitialValue { get; }

        public int CurrentValue => InitialValue + Modifier + PurchasedLevels;

        public int Modifier { get; set; }

        public int PurchasedLevels { get; set; }

        public CharacterStat(int initialValue)
        {
            InitialValue = initialValue;
        }
    }

    
}
