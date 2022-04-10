using System;

namespace Data.Stats
{
    public class Spirit : CharacterStat
    {
        public override Type Type => typeof(Spirit);
        public Spirit(int initialValue) : base(initialValue)
        {
        }
    }

    
}
