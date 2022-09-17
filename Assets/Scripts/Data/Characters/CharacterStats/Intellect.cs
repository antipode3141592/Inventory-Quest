using System;

namespace Data.Characters
{
    public class Intellect : CharacterStat
    {
        public override Type Type => typeof(Intellect);
        public override CharacterStatTypes Id => CharacterStatTypes.Intellect;
        public Intellect(int initialValue) : base(initialValue)
        {
        }
    }

    
}
