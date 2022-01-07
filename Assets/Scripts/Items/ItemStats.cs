using System;
using System.Collections.Generic;

namespace InventoryQuest
{
    [Serializable]
    public class ItemStats
    {
        public string ItemId;
        public float Weight;
        public float GoldValue;
        
        public string Description;
        public List<Modifier> Modifiers;

        public ItemStats(string itemId, float weight, float goldValue, string description, List<Modifier> modifiers = null)
        {
            ItemId = itemId;
            Weight = weight;
            GoldValue = goldValue;
            Description = description;
            Modifiers = modifiers;
        }
    }


    public class Modifier
    {
        public CharacterStat CharacterStat;
        public int Adjustment;
    }
}
