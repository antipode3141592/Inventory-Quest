using Data.Interfaces;
using System;

namespace Data
{
    public interface ICharacter
    {
        IContainer Backpack { get; }
        float CurrentEncumbrance { get; }
        string GuId { get; }
        bool IsIncapacitated { get; }

        void OnContainerChangedHandler(object sender, EventArgs e);
        void OnEquipHandler(object sender, ModifierEventArgs e);
        void OnUnequipHandler(object sender, ModifierEventArgs e);
    }
}