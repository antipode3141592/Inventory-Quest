using Data.Characters;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Items.Components
{
    public class EncounterLengthEffectStats: IUsableStats
    {
        [SerializeField] List<StatModifier> statModifiers;
        [SerializeField] List<ResistanceModifier> resistanceModifiers;
        [SerializeField] bool isConsumable = true;
        public List<StatModifier> StatModifiers => statModifiers;
        public List<ResistanceModifier> ResistanceModifiers => resistanceModifiers;
        public bool IsConsumable => isConsumable;
    }
}
