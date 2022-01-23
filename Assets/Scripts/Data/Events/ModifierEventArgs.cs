using System;
using System.Collections.Generic;

namespace Data
{
    public class ModifierEventArgs : EventArgs
    {
        public List<StatModifier> Modifiers;

        public ModifierEventArgs(List<StatModifier> modifiers)
        {
            Modifiers = modifiers;
        }
    }
}
