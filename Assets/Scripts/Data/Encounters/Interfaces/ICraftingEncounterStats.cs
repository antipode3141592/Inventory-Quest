using System.Collections.Generic;

namespace Data.Encounters
{
    public interface ICraftingEncounterStats: IEncounterStats
    {
        public List<string> RequiredItemIds { get; }
    }
}
