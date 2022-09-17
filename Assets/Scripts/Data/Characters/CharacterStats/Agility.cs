using System;

namespace Data.Characters
{
    public class Agility : CharacterStat
    {
        public override Type Type => typeof(Agility);
        public override CharacterStatTypes Id => CharacterStatTypes.Agility;
        public Agility(int initialValue) : base(initialValue)
        {
        }
    }

    
}
