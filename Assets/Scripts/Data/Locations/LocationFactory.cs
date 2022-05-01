using Data.Characters;
using Zenject;

namespace Data.Locations
{
    public class LocationFactory
    {

        public static Location GetLocation(ILocationStats locationStats)
        {
            return new Location(locationStats);
        }
    }
}
