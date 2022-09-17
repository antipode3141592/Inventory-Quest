using System;

namespace Data.Characters
{
    public class Charisma : CharacterStat
    {
        public override Type Type => typeof(Charisma);
        public override CharacterStatTypes Id => CharacterStatTypes.Charisma;
        public Charisma(int initialValue) : base(initialValue)
        {
        }
    }

    
}
