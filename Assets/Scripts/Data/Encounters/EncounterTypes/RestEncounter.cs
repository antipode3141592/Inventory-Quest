using Data.Characters;

namespace Data.Encounters
{
    public class RestEncounter : Encounter
    {
        public RestEncounter(IEncounterStats encounterStats) : base(encounterStats)
        {
        }

        public override bool Resolve(Party party)
        {
            foreach(var character in party.Characters.Values)
            {

                //restore health and magic
                character.Stats.CurrentHealth = character.Stats.MaximumHealth;
            }
            return true;
        }
    }
}
