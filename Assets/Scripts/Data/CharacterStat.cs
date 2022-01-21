using System;

namespace Data
{
    [Serializable]
    public class CharacterStat
    {
        public float InitialValue { get; }

        public float CurrentValue { get; set; }

        public CharacterStat(float initialValue)
        {
            InitialValue = initialValue;
        }
    }
}
