namespace Data.Items.Components
{
    public interface IUsableStats: IItemComponentStats
    {
        public bool IsConsumable { get; }
    }
}
