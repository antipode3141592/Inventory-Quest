using Data;
using Data.Encounters;
using Data.Interfaces;
using InventoryQuest.Characters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestEncounters
    {
        IItemDataSource itemDataSource;
        ICharacterDataSource characterDataSource;
        IEncounterDataSource encounterDataSource;
        IEncounter currentEncounter;
        Party party;
        Character partyMember;
        CharacterStats playerStats;
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

            currentEncounter = EncounterFactory.GetEncounter(encounterDataSource.GetEncounterById(encounterId));

            playerStats = characterDataSource.GetCharacterStats("Player");
            backpackStats = (EquipableContainerStats)itemDataSource.GetItemStats("adventure backpack");
            backpack = (IEquipable)ItemFactory.GetItem(backpackStats);
            EquipableStats = (EquipableItemStats)itemDataSource.GetItemStats("basic_sword_15");
            partyMember = CharacterFactory.GetCharacter(
                characterStats: playerStats, 
                startingEquipment: new IEquipable[]{ backpack });
            party = new Party(new Character[] { partyMember });
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
            partyMember.EquipmentSlots[equipableItem.SlotType].TryEquip(out var previousItem, equipableItem);
            Assert.IsTrue(currentEncounter.Resolve(party));
        }
        
        [Test]
        public void TestOfMightSuccessParty()
        {
            party.AddCharacter(CharacterFactory.GetCharacter(playerStats, new IEquipable[] { (IEquipable)ItemFactory.GetItem(backpackStats)}));
            party.AddCharacter(CharacterFactory.GetCharacter(playerStats, new IEquipable[] { (IEquipable)ItemFactory.GetItem(backpackStats) }));
            party.AddCharacter(CharacterFactory.GetCharacter(playerStats, new IEquipable[] { (IEquipable)ItemFactory.GetItem(backpackStats) }));
            party.AddCharacter(CharacterFactory.GetCharacter(playerStats, new IEquipable[] { (IEquipable)ItemFactory.GetItem(backpackStats) }));
            party.AddCharacter(CharacterFactory.GetCharacter(playerStats, new IEquipable[] { (IEquipable)ItemFactory.GetItem(backpackStats) }));
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
            IEncounter encounter = (IEncounter)EncounterFactory.GetEncounter(encounterDataSource.GetRandomEncounter());
            Assert.IsTrue(encounter is IEncounter);
        }

    }
}
