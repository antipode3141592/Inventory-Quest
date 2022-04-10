using Data.Characters;
using Data.Quests;
using Data.Items;
using System;
using System.Linq;

namespace InventoryQuest.Quests
{
    public class GatheringQuest : IQuest
    {
        

        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public string TargetItemId { get; }

        public int TargetQuantity { get; }

        public string RewardId { get; }

        public GatheringQuest(GatheringQuestStats stats)
        {
            Id = Guid.NewGuid().ToString();
            Name = stats.Name;
            Description = stats.Description;
            TargetItemId = stats.TargetItemId;
            TargetQuantity = stats.TargetQuantity;
            RewardId = stats.RewardId;
        }

        public bool Evaluate(Party party)
        {
            int targetCount = GetPartyItemCountById(party, TargetItemId);
            if (targetCount == TargetQuantity) return true;
            return false;
        }

        public int GetItemCountById(PlayableCharacter character, string id)
        {
            var count = 0;
            count += character.Backpack.Contents.Count(x => x.Value.Item.Id == id);
            count += character.EquipmentSlots.Count(x => x.Value.EquippedItem != null && ((IItem)x.Value.EquippedItem).Id == id);
            return count;
        }

        public int GetPartyItemCountById(Party party, string id)
        {
            int partyCount = 0;
            foreach (var character in party.Characters.Values)
            {
                partyCount += GetItemCountById(character, id);
            }
            return partyCount;
        }

        public void Cancel()
        {

        }
    }
}
