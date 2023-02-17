using System;
using System.Collections.Generic;

namespace Data.Characters
{
    public class ModifierEventArgs : EventArgs
    {
        public IList<StatModifier> StatModifiers;
        public IList<ResistanceModifier> ResistanceModifiers;

        public ModifierEventArgs(IList<StatModifier> statModifiers, IList<ResistanceModifier> resistanceModifiers)
        {
            StatModifiers = statModifiers;
            ResistanceModifiers = resistanceModifiers;
        }
    }
}
