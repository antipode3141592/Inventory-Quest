namespace Data.Encounters
{
    public class PathFactory
    {
        public static IPath GetPath(IPathStats stats)
        {
            return new Path(stats);
        }
    }
}