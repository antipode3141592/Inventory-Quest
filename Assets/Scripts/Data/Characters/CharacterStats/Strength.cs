using System;

namespace Data.Characters
{
    public class Strength : CharacterStat
    {
        public override Type Type => typeof(Strength);
        public override StatTypes Id => StatTypes.Strength;
        public Strength(int initialValue) : base(initialValue)
        {
        }
    }

    
}
