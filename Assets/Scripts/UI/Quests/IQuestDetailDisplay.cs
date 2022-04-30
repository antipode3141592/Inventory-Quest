using Data.Quests;

namespace InventoryQuest.UI.Quests
{
    public interface IQuestDetailDisplay
    {
        public void UpdateDisplay();

        public void SetDisplay(IQuestStats questStats);
    }
}