using System.Collections.Generic;

namespace Data.Encounters
{
    public interface IEncounterStats
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public List<IChoice> Choices { get; }
    }
}
