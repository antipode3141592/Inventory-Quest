using Data.Items;
using System;

namespace Data.Characters
{
    public interface ICharacter
    {
        ContainerBase Backpack { get; }
        float CurrentEncumbrance { get; }
        string GuId { get; }
        bool IsIncapacitated { get; }

        void OnBackpackChangedHandler(object sender, EventArgs e);
        void OnEquipHandler(object sender, ModifierEventArgs e);
        void OnUnequipHandler(object sender, ModifierEventArgs e);
    }
}