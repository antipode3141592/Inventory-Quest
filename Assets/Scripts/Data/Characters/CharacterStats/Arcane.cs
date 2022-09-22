using System;

namespace Data.Characters
{
    public class Arcane : CharacterStat
    {
        public override Type Type => typeof(Arcane);
        public override StatTypes Id => StatTypes.Arcane;
        public Arcane(int initialValue) : base(initialValue)
        {

        }
    }

    
}
