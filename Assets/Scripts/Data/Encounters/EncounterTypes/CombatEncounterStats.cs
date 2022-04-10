using Data.Characters;
using System;
using System.Collections.Generic;


namespace Data.Encounters
{
    public class CombatEncounterStats : IEncounterStats
    {
        public string Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public int Experience { get; }

        public string Category => "Combat";

        public IList<string> RewardIds => throw new NotImplementedException();

        public IList<Func<bool>> Requirements => throw new NotImplementedException();

        public IList<string> PenaltyIds => throw new NotImplementedException();

        public IList<string> CharacterIds { get; }

        public IList<ICharacter> Characters { get; }

        public string SuccessMessage => throw new NotImplementedException();

        public string FailureMessage => throw new NotImplementedException();
    }
}
