using System;

namespace Data.Characters
{
    public class Intimidate : SkillStat
    {
        public Intimidate(int initialValue) : base(initialValue)
        {
        }

        public override Type Type => typeof(Intimidate);
        public override CharacterStatTypes Id => CharacterStatTypes.Intimidate;
    }
}
