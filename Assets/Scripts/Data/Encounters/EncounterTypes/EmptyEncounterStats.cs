using System.Collections.Generic;

namespace Data.Encounters
{
    public class EmptyEncounterStats : IEncounterStats
    {
        string _id;
        string _name;
        string _description;

        public EmptyEncounterStats(string id)
        {
            _id = id;
        }

        public string Id => _id;
        public string Name => _name;
        public string Description => _description;
        public List<IChoice> Choices { get; }
    }
}
