using System.Collections.Generic;
using System;

namespace Data.Characters
{
    public class Initiative : CompositeStat
    {
        public override Type Type => typeof(Initiative);
        public override CharacterStatTypes Id => CharacterStatTypes.Initiative;
        public Initiative(int initialValue, ICollection<IStat> stat) : base(initialValue, stat)
        {
        }
    }
}
