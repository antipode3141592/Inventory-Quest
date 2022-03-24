using Data;
using Data.Encounters;
using Data.Interfaces;
using InventoryQuest.Characters;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestEncounters
    {
        IDataSource dataSource;
        IEncounterDataSource encounterDataSource;
        IEncounter currentEncounter;
        Party party;
        Character partyMember;
        CharacterStats playerStats;
        ContainerStats backpackStats;
        EquipableItem equipableItem;
        

        const string encounterId = "test_of_might";

        [SetUp]
        public void SetUp()
        {
            EquipableItemStats EquipableStats;
            dataSource = new DataSourceTest();
            encounterDataSource = new EncounterDataSourceTest();

            currentEncounter = EncounterFactory.GetEncounter(encounterDataSource.GetEncounterById(encounterId));

            playerStats = dataSource.GetCharacterStats("Player");
            backpackStats = (ContainerStats)dataSource.GetItemStats("adventure backpack");
            EquipableStats = (EquipableItemStats)dataSource.GetItemStats("basic_sword_15");
            partyMember = CharacterFactory.GetCharacter(playerStats, backpackStats);
            party = new Party(new Character[] { partyMember });
            equipableItem = (EquipableItem)ItemFactory.GetItem(EquipableStats);

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
            party.AddCharacter(CharacterFactory.GetCharacter(playerStats, backpackStats));
            party.AddCharacter(CharacterFactory.GetCharacter(playerStats, backpackStats));
            party.AddCharacter(CharacterFactory.GetCharacter(playerStats, backpackStats));
            party.AddCharacter(CharacterFactory.GetCharacter(playerStats, backpackStats));
            party.AddCharacter(CharacterFactory.GetCharacter(playerStats, backpackStats));
            Assert.IsTrue(currentEncounter.Resolve(party));
        }

        [Test]
        public void TestOfMightFail()
        {
            Assert.IsFalse(currentEncounter.Resolve(party));
        }



    }
}
