namespace Data.Items
{
    public interface IStackable: IItemComponent
    {
        public int MinStackSize { get;}
    }
}
