using System;

namespace Data.Characters
{
    public class Swim : SkillStat
    {
        public Swim(int initialValue) : base(initialValue)
        {
        }

        public override Type Type => typeof(Swim);
        public override StatTypes Id => StatTypes.Swim;
    }
}
