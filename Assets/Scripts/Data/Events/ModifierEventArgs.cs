using Data.Stats;
using System;
using System.Collections.Generic;

namespace Data
{
    public class ModifierEventArgs : EventArgs
    {
        public IList<StatModifier> Modifiers;

        public ModifierEventArgs(IList<StatModifier> modifiers)
        {
            Modifiers = modifiers;
        }
    }
}
