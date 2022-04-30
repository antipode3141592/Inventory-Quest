using System;
using System.Collections.Generic;

namespace Data.Encounters
{
    [Serializable]

    public class EncounterDataList
    {
        public List<SkillCheckEncounterData> SkillCheckEncounters = new List<SkillCheckEncounterData>();
        public List<RestEncounterData> RestEncounters = new List<RestEncounterData>();

        public EncounterDataList(List<IEncounterStats> encounters)
        {
            foreach (var encounter in encounters)
            {
                var skillEncounter = encounter as SkillCheckEncounterStats;
                if (skillEncounter is not null)
                    SkillCheckEncounters.Add(new SkillCheckEncounterData(skillEncounter));
                var restEncounter = encounter as RestEncounterStats;
                if (restEncounter is not null)
                    RestEncounters.Add(new RestEncounterData(restEncounter));
            }
        }
    }
}
