using System;
using System.Collections.Generic;


namespace Data.Encounters
{
    public class CraftingEncounterStats : IEncounterStats
    {
        public string Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();
        
        public int Experience { get; }

        public string Category => "Crafting";

        public IList<string> RewardIds => throw new NotImplementedException();

        public IList<Func<bool>> Requirements => throw new NotImplementedException();

        public IList<string> PenaltyIds => throw new NotImplementedException();

        public IList<string> RequiredItemIds { get; }

        public string SuccessMessage { get; }

        public string FailureMessage { get; }
    }
}
