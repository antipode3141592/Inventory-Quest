using Data.Characters;

namespace Data.Items.Components
{
    public class EncounterLengthEffect : IUsable
    {
        EncounterLengthEffectStats encounterLengthEffectStats;

        public EncounterLengthEffectStats EncounterLengthEffectStats => encounterLengthEffectStats;

        public IItem Item { get; }

        public bool IsConsumable => encounterLengthEffectStats.IsConsumable;

        public bool HasBeenUsed { get; protected set; }

        public EncounterLengthEffect(EncounterLengthEffectStats encounterLengthEffectStats, IItem parentItem)
        {
            this.encounterLengthEffectStats = encounterLengthEffectStats;
            Item = parentItem;
        }

        public bool TryUse(ref ICharacter usedByCharacter)
        {
            if (HasBeenUsed)
                return false;
            usedByCharacter.ApplyModifiers(encounterLengthEffectStats.StatModifiers);
            usedByCharacter.ApplyModifiers(encounterLengthEffectStats.ResistanceModifiers);
            if (IsConsumable)
                Item.Quantity--;
            else
                HasBeenUsed = true;
            return true;
        }

        public void ResetUsage()
        {
            HasBeenUsed = false;
        }
    }
}
