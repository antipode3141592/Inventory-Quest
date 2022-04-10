using System;

namespace Data.Characters
{
    public class Strength : CharacterStat
    {
        public override Type Type => typeof(Strength);
        public Strength(int initialValue) : base(initialValue)
        {
        }
    }

    
}
