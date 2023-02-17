using Data.Characters;
using Data.Encounters;
using Data.Items;
using NUnit.Framework;
using System.Linq;
using Zenject;
using UnityEngine.TestTools;
using System.Collections;
using InventoryQuest.Managers;

namespace InventoryQuest.Testing
{
    public class TestSkillCheckEncounterFromScriptableObjects: SceneTestFixture
    {
        readonly string itemId = "";
        readonly string sceneName = "Test_Encounters";

        IItemDataSource itemDataSource;
        ICharacterDataSource characterDataSource;
        IEncounterDataSource encounterDataSource;
        IEncounter currentEncounter;
        Party party;
        ICharacter partyMember;
        ICharacterStats playerStats;
        IItemStats backpackStats;
        IItem equipableItem;
        IItem backpack;

        const string encounterId = "test_of_might";

        //public void CommonInstall()
        //{
        //    IItemStats EquipableStats;
        //    itemDataSource = new ItemDataSourceTest();
        //    characterDataSource = new CharacterDataSourceTest();
        //    //encounterDataSource = new EncounterDataSourceTest();

        //    currentEncounter = EncounterFactory.GetEncounter(encounterDataSource.GetById(encounterId));

        //    playerStats = characterDataSource.GetById("Player");
        //    backpackStats = itemDataSource.GetById("adventure backpack");
        //    backpack = ItemFactory.GetItem(backpackStats);
        //    EquipableStats = itemDataSource.GetById("basic_sword_15");
        //    partyMember = CharacterFactory.GetCharacter(
        //        baseStats: playerStats, 
        //        startingEquipment: new IItem[]{ backpack });
        //    party = new Party(new ICharacter[] { partyMember });
        //    equipableItem = ItemFactory.GetItem(EquipableStats);

        //}

        

        //[TearDown]
        //public void TearDown()
        //{
        //    encounterDataSource = null;
        //    currentEncounter = null;
        //    party = null;
        //    partyMember = null;
        //    equipableItem = null;
        //}

        //[UnityTest]
        //public IEnumerator TestItemShapeRetrievable()
        //{
        //    CommonInstall();

        //    yield return LoadScene(sceneName);

        //    var partyManager = SceneContainer.Resolve<IPartyManager>();
        //    var encounterManager = SceneContainer.Resolve<IEncounterManager>();

        //    var itemDataSource = SceneContainer.Resolve<IItemDataSource>();


        //}

        //[Test]
        //public void EncounterCreateSuccess()
        //{
        //    Assert.AreEqual(encounterId, currentEncounter.Id);
        //}

        //[Test]
        //public void TestOfMightSuccessSingle()
        //{
        //    var equipmentSlotType = (IEquipable)equipableItem.Components[typeof(IEquipable)].SlotType;
        //    partyMember.EquipmentSlots.Values.First(x => x.SlotType == equipmentSlotType).TryEquip(out var previousItem, equipableItem);
        //    Assert.IsTrue(currentEncounter.Resolve(party));
        //}
        
        //[Test]
        //public void TestOfMightSuccessParty()
        //{
        //    party.AddCharacter(CharacterFactory.GetCharacter(playerStats, new IItem[] { ItemFactory.GetItem(backpackStats)}));
        //    party.AddCharacter(CharacterFactory.GetCharacter(playerStats, new IItem[] { ItemFactory.GetItem(backpackStats) }));
        //    party.AddCharacter(CharacterFactory.GetCharacter(playerStats, new IItem[] { ItemFactory.GetItem(backpackStats) }));
        //    party.AddCharacter(CharacterFactory.GetCharacter(playerStats, new IItem[] { ItemFactory.GetItem(backpackStats) }));
        //    party.AddCharacter(CharacterFactory.GetCharacter(playerStats, new IItem[] { ItemFactory.GetItem(backpackStats) }));
        //    Assert.IsTrue(currentEncounter.Resolve(party));
        //}

        //[Test]
        //public void TestOfMightFail()
        //{
        //    Assert.IsFalse(currentEncounter.Resolve(party));
        //}

        //[Test]
        //public void RandomEncounterCreateSuccess()
        //{
        //    IEncounter encounter = EncounterFactory.GetEncounter(encounterDataSource.GetRandom());
        //    Assert.IsTrue(encounter is IEncounter);
        //}

    }
}
