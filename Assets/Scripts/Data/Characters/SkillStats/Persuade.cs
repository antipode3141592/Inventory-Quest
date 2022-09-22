using System;

namespace Data.Characters
{
    public class Persuade : SkillStat
    {
        public Persuade(int initialValue) : base(initialValue)
        {
        }

        public override Type Type => typeof(Persuade);
        public override StatTypes Id => StatTypes.Persuade;
    }
}
