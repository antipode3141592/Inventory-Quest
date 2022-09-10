using Data.Characters;
using Data.Items;
using System;
using System.Collections.Generic;

namespace Data.Quests
{
    public class DeliveryQuest : Quest
    {
        public DeliveryQuest(IDeliveryQuestStats stats) : base(stats)
        {
            ItemIds = stats.ItemIds;
            Quantities = stats.Quantities;
        }

        public List<string> ItemIds { get; }
        public List<int> Quantities { get; }

        public override bool Evaluate(Party party)
        {
            bool retval = true;
            for (int i = 0; i < ItemIds.Count; i++)
            {
                int runningTotal = 0;
                foreach (var character in party.Characters)
                {
                    foreach (var content in character.Value.Backpack.Contents)
                    {
                        if (content.Value.Item.Id == ItemIds[i])
                        {
                            runningTotal += content.Value.Item.Quantity;
                        }
                    }
                    foreach (var slot in character.Value.EquipmentSlots)
                    {
                        var equippedItem = slot.Value.EquippedItem as IItem;
                        if (slot.Value.EquippedItem is not null)
                        { 
                            if (equippedItem.Id == ItemIds[i])
                            {
                                runningTotal += equippedItem.Quantity;
                            }

                        }
                    }
                }
                retval = retval && (runningTotal >= Quantities[i]);
            }
            return retval;
        }
    }
}
