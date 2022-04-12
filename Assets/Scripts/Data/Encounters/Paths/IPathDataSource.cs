namespace Data.Encounters
{
    public interface IPathDataSource
    {
        public IPathStats GetPathById(string id);
    }
}