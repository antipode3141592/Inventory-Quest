using System;

namespace Data.Stats
{
    public class Agility : CharacterStat
    {
        public override Type Type => typeof(Agility);
        public Agility(int initialValue) : base(initialValue)
        {
        }
    }

    
}
