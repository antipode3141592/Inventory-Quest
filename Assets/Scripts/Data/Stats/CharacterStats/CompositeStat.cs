using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public abstract class CompositeStat : IStat
    {
        public int InitialValue { get; }

        public int Modifier { get; set; }

        public int CurrentValue => InitialValue + Modifier + ConnectedStats.Sum(x => x.CurrentValue);

        public ICollection<IStat> ConnectedStats { get; }

        public CompositeStat(int initialValue, ICollection<IStat> stat)
        {
            InitialValue = initialValue;
            ConnectedStats = stat;
        }
    }

    public class Attack : CompositeStat
    {
        public Attack(int initialValue, ICollection<IStat> stat) : base(initialValue, stat)
        {
        }
    }

    public class Defense : CompositeStat
    {
        public Defense(int initialValue, ICollection<IStat> stat) : base(initialValue, stat)
        {
        }
    }
}
