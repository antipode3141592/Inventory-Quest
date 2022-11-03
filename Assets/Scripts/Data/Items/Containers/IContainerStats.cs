namespace Data.Items
{
    public interface IContainerStats: IItemComponentStats
    {
        public Coor ContainerSize { get; }
    }
}
