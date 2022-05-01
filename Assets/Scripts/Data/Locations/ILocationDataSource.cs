namespace Data.Locations
{
    public interface ILocationDataSource
    {
        public ILocationStats GetLocationById(string id);
    }
}