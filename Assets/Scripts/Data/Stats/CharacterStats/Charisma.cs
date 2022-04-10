using System;

namespace Data.Stats
{
    public class Charisma : CharacterStat
    {
        public override Type Type => typeof(Charisma);

        public Charisma(int initialValue) : base(initialValue)
        {
        }
    }

    
}
