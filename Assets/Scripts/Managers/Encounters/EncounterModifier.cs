using Data;
using Data.Characters;
using Data.Items.Components;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public class EncounterModifier
    {
        public EncounterModifier(ICharacter character, List<StatModifier> statModifiers, List<ResistanceModifier> resistanceModifiers, EncounterLengthEffect encounterLengthEffect)
        {
            Character = character;
            StatModifiers = statModifiers;
            ResistanceModifiers = resistanceModifiers;
            EncounterLengthEffect = encounterLengthEffect;
        }

        public EncounterLengthEffect EncounterLengthEffect { get; }
        public ICharacter Character { get; }
        public List<StatModifier> StatModifiers { get; } = new();
        public List<ResistanceModifier> ResistanceModifiers { get; } = new();
    }
}
