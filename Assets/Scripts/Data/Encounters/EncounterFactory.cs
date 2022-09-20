namespace Data.Encounters
{
    public class EncounterFactory
    {
        public static IEncounter GetEncounter(IEncounterStats encounterStats)
        {
            var combatStats = encounterStats as ICombatEncounterStats;
            if (combatStats is not null) return new CombatEncounter(combatStats);
            var craftingStats = encounterStats as ICraftingEncounterStats;
            if (craftingStats is not null) return new CraftingEncounter(craftingStats);
            var skillCheckStats = encounterStats as ISkillCheckEncounterStats;
            if (skillCheckStats is not null) return new SkillCheckEncounter(skillCheckStats);
            var restStats = encounterStats as IRestEncounterStats;
            if (restStats is not null) return new RestEncounter(restStats);
            return null;
        }

    }
}
