using System.Collections.Generic;
using System;

namespace Data.Characters
{
    public class SpiritAffinity : CompositeStat
    {
        public override Type Type => typeof(SpiritAffinity);
        public override CharacterStatTypes Id => CharacterStatTypes.SpiritAffinity;
        public SpiritAffinity(int initialValue, ICollection<IStat> stat) : base(initialValue, stat)
        {

        }
    }
}
