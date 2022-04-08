﻿using System;

namespace Data.Stats
{
    [Serializable]
    public class DamageResistance
    {
        public DamageType DamageType;
        
        public float InitialValue;

        public DamageResistance(DamageType damageType, float initialValue)
        {
            DamageType = damageType;
            InitialValue = initialValue;
        }

        public float CurrentValue { get; }
    }
}
