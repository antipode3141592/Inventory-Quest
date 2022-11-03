using System;
using Data.Health;

namespace Data.Characters
{
    //  flat reduction
    //      damage received = incoming damage - relevant resistance type - armor if damage is "normal"
    [Serializable]
    public class DamageResistance
    {
        public DamageType DamageType;
        
        public int InitialValue;

        public DamageResistance(DamageType damageType, int initialValue)
        {
            DamageType = damageType;
            InitialValue = initialValue;
        }

        public int CurrentValue { get; }
    }
}
