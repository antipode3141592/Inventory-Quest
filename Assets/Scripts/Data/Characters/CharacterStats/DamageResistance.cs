using System;
using Data.Health;

namespace Data.Characters
{
    //  flat reduction
    //      damage received = incoming damage - relevant resistance type
    [Serializable]
    public class DamageResistance: IStat
    {
        public DamageType DamageType;
        public int InitialValue { get; set; }
        public int CurrentValue => InitialValue + Modifier + PurchasedLevels;
        public int PurchasedLevels { get; set; }
        public int Modifier { get; set; }

        public DamageResistance(DamageType damageType, int initialValue)
        {
            DamageType = damageType;
            InitialValue = initialValue;
        }
    }
}
