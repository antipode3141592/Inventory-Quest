using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Characters
{
    public abstract class CompositeStat : IStat
    {
        public virtual Type Type { get; } 

        public virtual StatTypes Id { get; }
        public int InitialValue { get; }

        public int Modifier { get; set; }

        public int CurrentValue => InitialValue + Modifier + PurchasedLevels + ConnectedStats.Sum(x => x.CurrentValue);

        public ICollection<IStat> ConnectedStats { get; }
        public int PurchasedLevels { get; set; }

        public CompositeStat(int initialValue, ICollection<IStat> stat)
        {
            InitialValue = initialValue;
            ConnectedStats = stat;
        }
    }
}
