using Data.Encounters;
using System.Collections.Generic;

namespace InventoryQuest.Testing
{
    class TestEncounterStats: IEncounterStats
    {
        protected string _id;
        protected string _name;
        protected string _description;
        protected List<IChoice> _choices = new();

        public TestEncounterStats(string id, string name, string description, List<IChoice> choices)
        {
            _id = id;
            _name = name;
            _description = description;
            _choices = choices;
        }

        public string Id => _id;
        public string Name => _name;
        public string Description => _description;
        public List<IChoice> Choices => _choices;
    }
}