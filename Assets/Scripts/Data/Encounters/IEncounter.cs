using Data.Characters;

namespace Data.Encounters
{
    public interface IEncounter
    {
        public string GuId { get; }
        public string Id { get; }
        public string Name { get; }
        public IEncounterStats Stats { get; }
        public IChoice ChosenChoice { get; }
        public bool Resolve(Party party);
        public void SetRequirement(IChoice choice);
    }
}