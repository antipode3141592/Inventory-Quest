using System.Collections.Generic;
using UnityEngine;

namespace Data.Items.Components
{
    public class EncounterLengthEffectStats: IUsableStats
    {
        [SerializeField] List<StatModifier> modifiers;
        [SerializeField] bool isConsumable = true;
        public List<StatModifier> Modifiers => modifiers;
        public bool IsConsumable => isConsumable;
    }
}
