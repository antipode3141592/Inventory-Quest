using System;

namespace Data.Characters
{
    public class Charisma : CharacterStat
    {
        public override Type Type => typeof(Charisma);

        public Charisma(int initialValue) : base(initialValue)
        {
        }
    }

    
}
