using Data.Characters;

namespace Data.Items.Components
{
    public class EncounterLengthEffect : IUsable
    {
        EncounterLengthEffectStats encounterLengthEffectStats;

        public EncounterLengthEffectStats EncounterLengthEffectStats => encounterLengthEffectStats;

        public IItem Item { get; }

        public bool IsConsumable => encounterLengthEffectStats.IsConsumable;

        public EncounterLengthEffect(EncounterLengthEffectStats encounterLengthEffectStats, IItem parentItem)
        {
            this.encounterLengthEffectStats = encounterLengthEffectStats;
            Item = parentItem;
        }

        public bool TryUse(ref ICharacter usedByCharacter)
        {
            usedByCharacter.ApplyModifiers(encounterLengthEffectStats.Modifiers);
            if (IsConsumable)
                Item.Quantity--;
            return true;
        }

        public override string ToString()
        {
            return $"";
        }
    }
}
