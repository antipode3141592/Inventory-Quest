using Data.Characters;
using Data.Items;
using System.Collections.Generic;

namespace Data.Encounters
{
    public static class EncounterResolutionHelpers
    {
        public static double CountItemInCharacterInventories(ICollection<ICharacter> characters, string itemId)
        {
            double runningTotal = 0;
            foreach (var character in characters)
            {
                foreach (var content in character.Backpack.Contents)
                {
                    if (content.Value.Item.Id == itemId)
                    {
                        runningTotal += content.Value.Item.Quantity;
                    }
                }
                foreach (var slot in character.EquipmentSlots)
                {
                    var equippedItem = slot.Value.EquippedItem as IItem;
                    if (slot.Value.EquippedItem is not null)
                    {
                        if (equippedItem.Id == itemId)
                        {
                            runningTotal += equippedItem.Quantity;
                        }

                    }
                }
            }
            return runningTotal;
        }
    }
}
