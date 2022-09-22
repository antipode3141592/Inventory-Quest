using System;

namespace Data.Characters
{
    public class Speed : CharacterStat
    {
        public override Type Type => typeof(Speed);
        public override StatTypes Id => StatTypes.Speed;
        public Speed(int initialValue) : base(initialValue)
        {
        }
    }

    
}
