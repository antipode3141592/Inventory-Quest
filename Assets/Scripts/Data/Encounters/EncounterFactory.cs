namespace Data.Encounters
{
    public class EncounterFactory
    {
        public static IEncounter GetEncounter(IEncounterStats encounterStats)
        {
            if (encounterStats is IRestEncounterStats restStats) 
                return new RestEncounter(restStats);
            if (encounterStats is IEmptyEncounterStats emptyStats) 
                return new EmptyEncounter(emptyStats);
            return new Encounter(encounterStats);
        }
    }
}
