using Data;
using Data.Characters;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public class EncounterModifier
    {
        public EncounterModifier(ICharacter character, List<StatModifier> modifiers)
        {
            Character = character;
            Modifiers = modifiers;
        }

        public ICharacter Character { get; }
        public List<StatModifier> Modifiers { get; } = new();
    }
}
