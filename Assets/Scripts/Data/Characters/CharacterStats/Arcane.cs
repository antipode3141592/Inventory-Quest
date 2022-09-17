using System;

namespace Data.Characters
{
    public class Arcane : CharacterStat
    {
        public override Type Type => typeof(Arcane);
        public override CharacterStatTypes Id => CharacterStatTypes.Arcane;
        public Arcane(int initialValue) : base(initialValue)
        {

        }
    }

    
}
