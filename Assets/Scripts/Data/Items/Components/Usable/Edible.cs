﻿using Data.Characters;

namespace Data.Items.Components
{
    public class Edible : IUsable
    {
        EdibleStats _edibleStats;
        
        public IItem Item { get; }

        public bool IsConsumable => true;

        public bool HasBeenUsed => false;

        public Edible(EdibleStats edibleStats, IItem parentItem)
        {
            this._edibleStats = edibleStats;
            Item = parentItem;
        }

        public bool TryUse(ref ICharacter useByCharacter)
        {
            useByCharacter.HealDamage(_edibleStats.RestorationStrength);
            if (IsConsumable)
                Item.Quantity--;
            return true;
        }

        public override string ToString()
        {
            return $"Restore {_edibleStats.RestorationStrength} health";
        }
    }
}
