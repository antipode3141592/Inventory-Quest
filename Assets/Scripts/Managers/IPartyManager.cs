using Data.Characters;

namespace InventoryQuest.Managers
{
    public interface IPartyManager
    {
        Party CurrentParty { get; }

        public void AddCharacterToParty(string id);
    }
}