using System;
using System.Collections.Generic;

namespace Data.Characters
{
    public class ArcaneAffinity : CompositeStat
    {
        public override Type Type => typeof(ArcaneAffinity);
        public override CharacterStatTypes Id => CharacterStatTypes.ArcaneAffinity;
        public ArcaneAffinity(int initialValue, ICollection<IStat> stat) : base(initialValue, stat)
        {

        }
    }
}
