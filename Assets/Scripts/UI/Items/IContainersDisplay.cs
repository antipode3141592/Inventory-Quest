namespace InventoryQuest.UI.Menus
{
    public interface IContainersDisplay
    {
        void DestroyContainers();
        void ContainerSelected(string containerGuid);
        void SetContainers();
    }
}