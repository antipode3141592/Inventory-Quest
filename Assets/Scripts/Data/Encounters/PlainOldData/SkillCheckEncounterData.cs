using System;
using System.Collections.Generic;

namespace Data.Encounters
{
    [Serializable]
    public class SkillCheckEncounterData: EncounterData
    {
        public List<SkillCheckRequirementData> SkillCheckRequirements = new();

        public SkillCheckEncounterData(SkillCheckEncounterStats stats) : base(stats)
        {
            foreach (var req in stats.SkillCheckRequirements)
            {
                SkillCheckRequirements.Add(new SkillCheckRequirementData(req));
            }
        }
    }
}
