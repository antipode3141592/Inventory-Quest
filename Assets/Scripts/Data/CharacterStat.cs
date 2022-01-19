using System;

namespace Data
{
    [Serializable]
    public class CharacterStat
    {
        public float InitialValue;

        public float CurrentValue { get; }

        public CharacterStat(float initialValue)
        {
            InitialValue = initialValue;
        }
    }
}
