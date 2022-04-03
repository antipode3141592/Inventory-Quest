namespace Data.Interfaces
{
    public interface IEncounterDataSource
    {
        public IEncounterStats GetEncounterById(string id);

        public IEncounterStats GetRandomEncounter();
    }
}
