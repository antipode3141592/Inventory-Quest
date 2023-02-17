using Data.Characters;
using System;

namespace Data.Encounters
{
    public class Encounter : IEncounter
    {
        IChoice chosenChoice;

        public string GuId { get; }
        public string Id => Stats.Id;
        public string Name => Stats.Name;
        public IEncounterStats Stats { get; }

        public virtual IChoice ChosenChoice => chosenChoice;

        public Encounter(IEncounterStats encounterStats)
        {
            GuId = Guid.NewGuid().ToString();
            Stats = encounterStats;
            chosenChoice = Stats.Choices[0];
        }

        public void SetRequirement(IChoice choice)
        {
            chosenChoice = choice;
        }

        public virtual bool Resolve(Party party)
        {
            if (chosenChoice.Resolve(party))
                return true;
            return false;
        }
    }
}
