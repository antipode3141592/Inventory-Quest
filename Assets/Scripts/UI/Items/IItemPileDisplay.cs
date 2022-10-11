namespace InventoryQuest.UI.Menus
{
    public interface IItemPileDisplay
    {
        void DestroyPiles();
        void PileSelected(string containerGuid);
        void SetPiles();
    }
}