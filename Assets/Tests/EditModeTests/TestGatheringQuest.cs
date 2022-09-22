using Data;
using Data.Characters;
using Data.Items;
using Data.Quests;
using NUnit.Framework;
using System.Collections.Generic;

namespace InventoryQuest.Testing
{
    public class TestGatheringQuest
    {
        IQuest quest;
        IGatheringQuestStats questStats;
        IItemDataSource itemDataSource;
        ICharacterDataSource characterDataSource;
        Party party;
        PlayableCharacter player;
        PlayableCharacter minion_1;
        PlayableCharacter minion_2;

        static int targetCount = 5;
        static string targetItemId = "apple_fuji";

        [SetUp]
        public void SetUp()
        {
            itemDataSource = new ItemDataSourceTest();
            characterDataSource = new CharacterDataSourceTest();
            player = (PlayableCharacter)CharacterFactory.GetCharacter(characterDataSource.GetById("Player"),
                new IEquipable[] { (IEquipable)ItemFactory.GetItem(itemDataSource.GetItemStats("adventure backpack")) });
            minion_1 = (PlayableCharacter)CharacterFactory.GetCharacter(characterDataSource.GetById("Minion"),
                new IEquipable[] { (IEquipable)ItemFactory.GetItem(itemDataSource.GetItemStats("adventure backpack")) });
            minion_2 = (PlayableCharacter)CharacterFactory.GetCharacter(characterDataSource.GetById("Minion"),
                new IEquipable[] { (IEquipable)ItemFactory.GetItem(itemDataSource.GetItemStats("adventure backpack")) });
            party = new Party(new PlayableCharacter[] {player, minion_1, minion_2});

            questStats = new GatheringQuestStats(
                id: "quest_000",
                name: "get_apples", 
                description: "gather 5 fuji apples",
                experience: 500,
                rewardId: "ring_charisma_1",
                sourceId: "",
                sourceType: QuestSourceTypes.Character,
                sinkId: "",
                sinkType: QuestSourceTypes.Character,
                targetQuantity: targetCount,
                targetItemId: targetItemId  
                );
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
            List<string> characterKeys = new(party.Characters.Keys);
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
