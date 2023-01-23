using Data;
using Data.Characters;
using Data.Items.Components;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public class EncounterModifier
    {
        public EncounterModifier(ICharacter character, List<StatModifier> modifiers, EncounterLengthEffect encounterLengthEffect)
        {
            Character = character;
            Modifiers = modifiers;
            EncounterLengthEffect = encounterLengthEffect;
        }

        public EncounterLengthEffect EncounterLengthEffect { get; }
        public ICharacter Character { get; }
        public List<StatModifier> Modifiers { get; } = new();
    }
}
