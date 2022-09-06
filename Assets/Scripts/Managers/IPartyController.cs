namespace InventoryQuest.Traveling
{
    public interface IPartyController
    {
        public float DistanceMoved { get; }

        public void MoveAll();

        public void IdleAll();

        public void PauseAll();
    }
}