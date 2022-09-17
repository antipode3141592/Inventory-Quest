using System;

namespace Data.Characters
{
    //Climb, Swim, Persuade, Intimidate 

    public class Climb : SkillStat
    {
        public Climb(int initialValue) : base(initialValue)
        {
        }

        public override Type Type => typeof(Climb);
        public override CharacterStatTypes Id => CharacterStatTypes.Climb;
    }
}
