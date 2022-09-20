using Data.Characters;
using System;
using System.Collections.Generic;


namespace Data.Encounters
{
    public class CombatEncounterStats : ICombatEncounterStats
    {
        public string Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public int Experience { get; }

        public string Category => "Combat";

        public List<string> RewardIds => throw new NotImplementedException();

        public List<Func<bool>> Requirements => throw new NotImplementedException();

        public List<string> PenaltyIds => throw new NotImplementedException();

        public List<string> CharacterIds { get; }

        public List<ICharacter> Characters { get; }

        public string SuccessMessage => throw new NotImplementedException();

        public string FailureMessage => throw new NotImplementedException();
    }
}
