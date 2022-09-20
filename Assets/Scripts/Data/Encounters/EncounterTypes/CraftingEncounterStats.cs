using System;
using System.Collections.Generic;


namespace Data.Encounters
{
    public class CraftingEncounterStats : ICraftingEncounterStats
    {
        public string Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();
        
        public int Experience { get; }

        public string Category => "Crafting";

        public List<string> RewardIds => throw new NotImplementedException();

        public List<string> PenaltyIds => throw new NotImplementedException();

        public List<string> RequiredItemIds { get; }

        public string SuccessMessage { get; }

        public string FailureMessage { get; }
    }
}
