using Data.Characters;

namespace Data.Encounters
{
    public class EmptyEncounter : Encounter
    {
        public EmptyEncounter(IEmptyEncounterStats encounterStats) : base(encounterStats)
        {
        }

        public override bool Resolve(Party party)
        {
            return true;
        }
    }
}
