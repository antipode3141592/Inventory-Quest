using System;

namespace Data.Stats
{
    public class Strength : CharacterStat
    {
        public override Type Type => typeof(Strength);
        public Strength(int initialValue) : base(initialValue)
        {
        }
    }

    
}
