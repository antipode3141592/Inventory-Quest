using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    [CreateAssetMenu(menuName = "InventoryQuest/EncounterStats/Skill", fileName = "skill")]
    public class SkillCheckEncounterStatsSO : EncounterStatsSO, ISkillCheckEncounterStats
    {
        [SerializeField] List<SkillCheckRequirement> skillCheckRequirements;

        public List<SkillCheckRequirement> SkillCheckRequirements => skillCheckRequirements;
    }


}