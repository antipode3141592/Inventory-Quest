using System.Collections.Generic;

namespace Data.Encounters
{
    public interface ISkillCheckEncounterStats: IEncounterStats
    {
        public List<SkillCheckRequirement> SkillCheckRequirements { get; }
    }
}
