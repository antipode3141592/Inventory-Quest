using System;

namespace Data.Characters
{
    public class Speed : CharacterStat
    {
        public override Type Type => typeof(Speed);
        public override CharacterStatTypes Id => CharacterStatTypes.Speed;
        public Speed(int initialValue) : base(initialValue)
        {
        }
    }

    
}
