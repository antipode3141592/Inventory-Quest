using System;

namespace Data.Characters
{
    public class Vitality : CharacterStat
    {
        public override Type Type => typeof(Vitality);
        public override CharacterStatTypes Id => CharacterStatTypes.Vitality;
        public Vitality(int initialValue) : base(initialValue)
        {
        }
    }

    
}
