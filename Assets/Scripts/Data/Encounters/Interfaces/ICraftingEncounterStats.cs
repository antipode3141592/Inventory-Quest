using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Encounters
{
    public interface ICraftingEncounterStats: IEncounterStats
    {
        public List<string> RequiredItemIds { get; }
    }
}
