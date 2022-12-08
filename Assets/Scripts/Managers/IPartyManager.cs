using Data.Characters;

namespace InventoryQuest.Managers
{
    public interface IPartyManager
    {
        Party CurrentParty { get; }

        public void AddCharacterToPartyById(string id);

        public void AddCharacterToParty(ICharacterStats characterStats);
    }
}