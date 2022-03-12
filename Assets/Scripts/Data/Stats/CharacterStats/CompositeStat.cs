using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public abstract class CompositeStat : IStat
    {
        public float InitialValue { get; }

        public float Modifier { get; set; }

        public float CurrentValue => InitialValue + Modifier + ConnectedStats.Sum(x => x.CurrentValue);

        public ICollection<IStat> ConnectedStats { get; }

        public CompositeStat(float initialValue, ICollection<IStat> stat)
        {
            InitialValue = initialValue;
            ConnectedStats = stat;
        }
    }

    public class Attack : CompositeStat
    {
        public Attack(float initialValue, ICollection<IStat> stat) : base(initialValue, stat)
        {
        }
    }

    public class Defense : CompositeStat
    {
        public Defense(float initialValue, ICollection<IStat> stat) : base(initialValue, stat)
        {
        }
    }
}
