using System;

namespace Data.Characters
{
    public class Vitality : CharacterStat
    {
        public override Type Type => typeof(Vitality);
        public Vitality(int initialValue) : base(initialValue)
        {
        }
    }

    
}
