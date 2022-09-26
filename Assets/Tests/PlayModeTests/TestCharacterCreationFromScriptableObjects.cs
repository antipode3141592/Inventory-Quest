using Data.Characters;
using Data.Items;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

public class TestCharacterCreationFromScriptableObjects : SceneTestFixture
{
    readonly string playerId = "player";
    readonly string sceneName = "Test_Characters";

    void CommonInstall()
    {

    }

    [UnityTest]
    public IEnumerator TestCharacterStatRetrievable()
    {
        CommonInstall();

        yield return LoadScene(sceneName);

        var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
        var playerStats = characterDataSource.GetById(playerId);
        Assert.That(playerStats is not null);
    }

    [UnityTest]
    public IEnumerator TestCharacterFactoryCreationSuccess()
    {
        CommonInstall();

        yield return LoadScene(sceneName);

        var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
        var playerStats = characterDataSource.GetById(playerId);
        var player = CharacterFactory.GetCharacter(playerStats);

        Assert.That(player is not null);
    }

    [UnityTest]
    public IEnumerator TestCreatedCharacterWithInitialStatsSuccess()
    {
        CommonInstall();

        yield return LoadScene(sceneName);

        var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
        var playerStats = characterDataSource.GetById(playerId);
        int addedStrength = playerStats.InitialStats[StatTypes.Strength];

        int targetStrength = playerStats.SpeciesBaseStats.BaseStats[StatTypes.Strength] + addedStrength;

        var player = CharacterFactory.GetCharacter(playerStats);

        Assert.IsTrue(player.StatDictionary[StatTypes.Strength].CurrentValue == targetStrength);
    }

    [UnityTest]
    public IEnumerator TestCreatedCharacterWithInitialEquipmentSuccess()
    {
        CommonInstall();

        yield return LoadScene(sceneName);

        var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
        var itemDataSource = SceneContainer.Resolve<IItemDataSource>();

        var playerStats = characterDataSource.GetById(playerId);
        

        var startingEquipment = new IEquipable[] {
            (IEquipable)ItemFactory.GetItem(itemDataSource.GetItemStats("adventure backpack")),
        };

        var player = CharacterFactory.GetCharacter(
            baseStats: playerStats,
            startingEquipment: startingEquipment);


        Assert.That(player.Backpack.Id == "adventure backpack");
        
    }

    [UnityTest]
    public IEnumerator TestCreatedCharacterWithStartingInventorySuccess()
    {
        CommonInstall();

        yield return LoadScene(sceneName);

        var characterDataSource = SceneContainer.Resolve<ICharacterDataSource>();
        var itemDataSource = SceneContainer.Resolve<IItemDataSource>();

        var playerStats = characterDataSource.GetById(playerId);


        var startingEquipment = new IEquipable[] {
            (IEquipable)ItemFactory.GetItem(itemDataSource.GetItemStats("adventure backpack")),
        };

        var startingInventory = new IItem[]{
            ItemFactory.GetItem(itemDataSource.GetItemStats("questitem_1")),
            ItemFactory.GetItem(itemDataSource.GetItemStats("apple_fuji")),
            ItemFactory.GetItem(itemDataSource.GetItemStats("apple_fuji")),
            ItemFactory.GetItem(itemDataSource.GetItemStats("apple_fuji")),
            ItemFactory.GetItem(itemDataSource.GetItemStats("ore_bloom_common")),
            ItemFactory.GetItem(itemDataSource.GetItemStats("ore_bloom_common")),
            ItemFactory.GetItem(itemDataSource.GetItemStats("ore_bloom_common"))
        };

        float targetWeight = ((IItem)startingEquipment[0]).Weight;
        foreach (var item in startingInventory)
        {
            targetWeight += item.Weight;
        }

        var player = CharacterFactory.GetCharacter(
            baseStats: playerStats,
            startingEquipment: startingEquipment,
            startingInventory: startingInventory);

        Assert.That(Mathf.Abs(player.CurrentEncumbrance - targetWeight) < 0.01);

    }


}