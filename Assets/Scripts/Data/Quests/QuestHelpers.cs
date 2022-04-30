using Data.Characters;
using Data.Items;
using System.Linq;

namespace Data.Quests
{
    internal static class QuestHelpers
    {

        public static int GetItemCountById(PlayableCharacter character, string id)
        {
            var count = 0;
            count += character.Backpack.Contents.Count(x => x.Value.Item.Id == id);
            count += character.EquipmentSlots.Count(x => x.Value.EquippedItem != null && ((IItem)x.Value.EquippedItem).Id == id);
            return count;
        }

        public static int GetPartyItemCountById(Party party, string id)
        {
            int partyCount = 0;
            foreach (var character in party.Characters.Values)
            {
                partyCount += GetItemCountById(character, id);
            }
            return partyCount;
        }
    }
}