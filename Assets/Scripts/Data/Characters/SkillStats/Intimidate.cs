using System;

namespace Data.Characters
{
    public class Intimidate : SkillStat
    {
        public Intimidate(int initialValue) : base(initialValue)
        {
        }

        public override Type Type => typeof(Intimidate);
        public override StatTypes Id => StatTypes.Intimidate;
    }
}
