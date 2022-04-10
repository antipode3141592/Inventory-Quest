using System;

namespace Data.Stats
{
    public class Speed : CharacterStat
    {
        public override Type Type => typeof(Speed);
        public Speed(int initialValue) : base(initialValue)
        {
        }
    }

    
}
