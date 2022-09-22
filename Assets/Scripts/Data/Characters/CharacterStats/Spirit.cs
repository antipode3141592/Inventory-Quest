using System;

namespace Data.Characters
{
    public class Spirit : CharacterStat
    {
        public override Type Type => typeof(Spirit);
        public override StatTypes Id => StatTypes.Spirit;
        public Spirit(int initialValue) : base(initialValue)
        {
        }
    }

    
}
