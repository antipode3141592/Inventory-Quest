using Data.Characters;
using System;

namespace Data.Encounters
{
    public class DecisionEncounter : Encounter
    {
        public DecisionEncounter(IEncounterStats encounterStats) : base(encounterStats)
        {
        }

        public override bool Resolve(Party party)
        {
            throw new NotImplementedException();
        }
    }
}
