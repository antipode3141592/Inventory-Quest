using System.Collections.Generic;


namespace Data.Encounters
{
    public class CraftingEncounter : Encounter
    {
        public CraftingEncounter(CraftingEncounterStats stats) : base (stats)
        {
            RequiredItemIds = stats.RequiredItemIds;
        }

        public IList<string> RequiredItemIds { get; }

        public override bool Resolve(Party party)
        {
            return false;
        }

    }
}
