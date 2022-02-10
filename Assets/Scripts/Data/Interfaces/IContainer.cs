namespace Data.Interfaces
{
    public interface IContainer
    {
        public bool TryPlace(IItem item, Coor target);
        public bool TryTake(out IItem item, Coor target);
    }


}
