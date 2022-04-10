using System;

namespace Data.Stats
{
    public class Arcane : CharacterStat
    {
        public override Type Type => typeof(Arcane);

        public Arcane(int initialValue) : base(initialValue)
        {

        }
    }

    
}
