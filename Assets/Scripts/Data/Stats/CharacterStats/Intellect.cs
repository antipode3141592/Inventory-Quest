using System;

namespace Data.Stats
{
    public class Intellect : CharacterStat
    {
        public override Type Type => typeof(Intellect);
        public Intellect(int initialValue) : base(initialValue)
        {
        }
    }

    
}
