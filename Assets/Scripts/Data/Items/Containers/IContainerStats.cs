namespace Data.Items
{
    public interface IContainerStats: IItemStats
    {
        public Coor ContainerSize { get; }
    }
}
