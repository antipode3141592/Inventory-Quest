using Data;
using Data.Interfaces;
using Data.Stats;
using InventoryQuest.Characters;
using InventoryQuest.Quests;
using NUnit.Framework;
using System.Collections.Generic;

namespace InventoryQuest.Testing
{
    public class TestGatheringQuest
    {
        IQuest quest;
        GatheringQuestStats questStats;
        IItemDataSource itemDataSource;
        ICharacterDataSource characterDataSource;
        Party party;
        Character player;
        Character minion_1;
        Character minion_2;

        static int targetCount = 5;
        static string targetItemId = "apple_fuji";

        [SetUp]
        public void SetUp()
        {
            itemDataSource = new ItemDataSourceTest();
            characterDataSource = new CharacterDataSourceTest();
            player = CharacterFactory.GetCharacter(characterDataSource.GetCharacterStats("Player"),
                new IEquipable[] { (IEquipable)ItemFactory.GetItem(itemDataSource.GetItemStats("adventure backpack")) });
            minion_1 = CharacterFactory.GetCharacter(characterDataSource.GetCharacterStats("Minion"),
                new IEquipable[] { (IEquipable)ItemFactory.GetItem(itemDataSource.GetItemStats("adventure backpack")) });
            minion_2 = CharacterFactory.GetCharacter(characterDataSource.GetCharacterStats("Minion"),
                new IEquipable[] { (IEquipable)ItemFactory.GetItem(itemDataSource.GetItemStats("adventure backpack")) });
            party = new Party(new Character[] {player, minion_1, minion_2});

            questStats = new GatheringQuestStats("quest_000","get_apples", "gather 5 fuji apples", targetCount, targetItemId,  "ring_charisma_1");
            quest = new GatheringQuest(questStats);
        }

        [TearDown]
        public void TearDown()
        {
            itemDataSource = null;
            player = null;
            minion_1 = null;
            minion_2 = null;
            quest = null;
            party = null;
        }

        void AddItems(string itemId, int itemsToMake)
        {
            List<string> characterKeys = new List<string>(party.Characters.Keys);
            for (int i = 0; i < itemsToMake; i++)
                party.Characters[characterKeys[i % characterKeys.Count]].Backpack
                     .TryPlace(ItemFactory.GetItem(itemDataSource.GetItemStats(itemId)), new Coor(0, i));

        }

        [Test]
        public void CreateQuest()
        {
            Assert.IsNotNull(quest);
            
        }

        [Test]
        public void QuestEvaluateTrue()
        {
            AddItems(targetItemId, 5);
            Assert.IsTrue(quest.Evaluate(party));
        }

        [Test]
        public void QuestEvaluateFalse()
        {
            AddItems(targetItemId, 4);
            Assert.IsFalse(quest.Evaluate(party));
        }
    }
}
