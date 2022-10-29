using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Penalties
{
    public interface IPenaltyStats
    {
        public PenaltyType PenaltyType { get; }
    }
}
