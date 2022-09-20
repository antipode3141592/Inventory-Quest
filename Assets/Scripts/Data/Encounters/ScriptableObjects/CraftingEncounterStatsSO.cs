using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    [CreateAssetMenu(menuName = "InventoryQuest/EncounterStats/Crafting", fileName = "craft")]
    public class CraftingEncounterStatsSO : EncounterStatsSO, ICraftingEncounterStats
    {
        [SerializeField] List<string> requiredItemIds;

        public List<string> RequiredItemIds => requiredItemIds;
    }
}