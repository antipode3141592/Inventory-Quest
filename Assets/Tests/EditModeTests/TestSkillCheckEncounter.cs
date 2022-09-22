using Data.Characters;
using Data.Encounters;
using Data.Items;
using NUnit.Framework;
using System.Linq;

namespace InventoryQuest.Testing
{
    public class TestSkillCheckEncounter
    {
        IItemDataSource itemDataSource;
        ICharacterDataSource characterDataSource;
        IEncounterDataSource encounterDataSource;
        IEncounter currentEncounter;
        Party party;
        PlayableCharacter partyMember;
        ICharacterStats playerStats;
        EquipableContainerStats backpackStats;
        IEquipable equipableItem;
        IEquipable backpack;

        const string encounterId = "test_of_might";

        [SetUp]
        public void SetUp()
        {
            EquipableItemStats EquipableStats;
            itemDataSource = new ItemDataSourceTest();
            characterDataSource = new CharacterDataSourceTest();
            encounterDataSource = new EncounterDataSourceTest();

            currentEncounter = EncounterFactory.GetEncounter(encounterDataSource.GetById(encounterId));

            playerStats = characterDataSource.GetById("Player");
            backpackStats = (EquipableContainerStats)itemDataSource.GetItemStats("adventure backpack");
            backpack = (IEquipable)ItemFactory.GetItem(backpackStats);
            EquipableStats = (EquipableItemStats)itemDataSource.GetItemStats("basic_sword_15");
            partyMember = (PlayableCharacter)CharacterFactory.GetCharacter(
                baseStats: playerStats, 
                startingEquipment: new IEquipable[]{ backpack });
            party = new Party(new PlayableCharacter[] { partyMember });
            equipableItem = (IEquipable)ItemFactory.GetItem(EquipableStats);

        }

        [TearDown]
        public void TearDown()
        {
            encounterDataSource = null;
            currentEncounter = null;
            party = null;
            partyMember = null;
            equipableItem = null;
        }

        [Test]
        public void EncounterCreateSuccess()
        {
            Assert.AreEqual(encounterId, currentEncounter.Id);
        }

        [Test]
        public void TestOfMightSuccessSingle()
        {
            partyMember.EquipmentSlots.Values.First(x => x.SlotType == equipableItem.SlotType).TryEquip(out var previousItem, equipableItem);
            Assert.IsTrue(currentEncounter.Resolve(party));
        }
        
        [Test]
        public void TestOfMightSuccessParty()
        {
            party.AddCharacter((PlayableCharacter)CharacterFactory.GetCharacter(playerStats, new IEquipable[] { (IEquipable)ItemFactory.GetItem(backpackStats)}));
            party.AddCharacter((PlayableCharacter)CharacterFactory.GetCharacter(playerStats, new IEquipable[] { (IEquipable)ItemFactory.GetItem(backpackStats) }));
            party.AddCharacter((PlayableCharacter)CharacterFactory.GetCharacter(playerStats, new IEquipable[] { (IEquipable)ItemFactory.GetItem(backpackStats) }));
            party.AddCharacter((PlayableCharacter)CharacterFactory.GetCharacter(playerStats, new IEquipable[] { (IEquipable)ItemFactory.GetItem(backpackStats) }));
            party.AddCharacter((PlayableCharacter)CharacterFactory.GetCharacter(playerStats, new IEquipable[] { (IEquipable)ItemFactory.GetItem(backpackStats) }));
            Assert.IsTrue(currentEncounter.Resolve(party));
        }

        [Test]
        public void TestOfMightFail()
        {
            Assert.IsFalse(currentEncounter.Resolve(party));
        }

        [Test]
        public void RandomEncounterCreateSuccess()
        {
            IEncounter encounter = (IEncounter)EncounterFactory.GetEncounter(encounterDataSource.GetRandom());
            Assert.IsTrue(encounter is IEncounter);
        }

    }
}
