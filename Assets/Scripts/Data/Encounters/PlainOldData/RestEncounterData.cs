using System;

namespace Data.Encounters
{
    [Serializable]
    public class RestEncounterData : EncounterData
    {
        public RestEncounterData(RestEncounterStats stats) : base(stats)
        {
        }
    }
}
