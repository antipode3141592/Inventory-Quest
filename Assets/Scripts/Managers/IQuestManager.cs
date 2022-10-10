namespace InventoryQuest.Managers
{
    public interface IQuestManager
    {
        public void AddItemToPartyInventory(string itemId);
        public double CountItemInPartyInventory(string itemId);
        public void RemoveItemFromPartyInventory(string itemId, double minToRemove);
    }
}