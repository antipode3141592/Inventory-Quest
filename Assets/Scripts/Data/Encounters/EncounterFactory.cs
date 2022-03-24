using Data.Interfaces;

namespace Data.Encounters
{
    public class EncounterFactory
    {
        public static IEncounter GetEncounter(IEncounterStats encounterStats)
        {
            var combatStats = encounterStats as CombatEncounterStats;
            if (combatStats is not null) return new CombatEncounter(combatStats);
            var craftingStats = encounterStats as CraftingEncounterStats;
            if (craftingStats is not null) return new CraftingEncounter(craftingStats);
            var skillCheckStats = encounterStats as SkillCheckEncounterStats;
            if (skillCheckStats is not null) return new SkillCheckEncounter(skillCheckStats);
            return null;
        }

    }
}
