using Data.Characters;

namespace InventoryQuest.Managers
{
    public interface IPartyManager
    {
        Party CurrentParty { get; }
        public bool IsPartyDead { get; set; }
        public void AddCharacterToPartyById(string id);
        public void AddCharacterToParty(ICharacterStats characterStats);
        public double CountItemInCharacterInventories(string itemId);
    }
}