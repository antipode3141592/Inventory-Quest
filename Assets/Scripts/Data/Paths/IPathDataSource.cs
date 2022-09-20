namespace Data.Encounters
{
    public interface IPathDataSource
    {
        public IPathStats GetPathById(string id);

        public IPathStats GetPathForStartAndEndLocations(string startLocationId, string endLocationId);
    }
}