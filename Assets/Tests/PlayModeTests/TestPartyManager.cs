using Data;
using Data.Characters;
using Data.Items;
using InventoryQuest.Managers;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

public class TestPartyManager : SceneTestFixture
{
    readonly string sceneName = "Test_PartyManager";

    readonly string swordId = "basic_sword_1";

    IItemDataSource itemDataSource;

    IPartyManager partyManager;

    IItemStats backpackStats;
    IItemStats swordStats;

    List<IItem> ItemsList;

    IItem backpack;
    IContainer backpackContainer;

    void CommonInstall()
    {
        ItemsList = new List<IItem>();
    }

    void CommonPostSceneLoadInstall()
    {
        itemDataSource = SceneContainer.Resolve<IItemDataSource>();
        partyManager = SceneContainer.Resolve<IPartyManager>();
        swordStats = itemDataSource.GetById(swordId);

        partyManager.AddCharacterToPartyById("player");
        backpackContainer = partyManager.CurrentParty.Characters.ElementAt(0).Value.Backpack;
    }

    void AddItemsToList(ref List<IItem> itemList, IItemStats stats, int qty = 1)
    {
        for (int i = 0; i < qty; i++)
            itemList.Add(ItemFactory.GetItem(itemStats: stats));
    }

    [UnityTest]
    public IEnumerator CountItemInPartyWithEquipmentSuccess()
    {
        CommonInstall();

        yield return LoadScene(sceneName);

        CommonPostSceneLoadInstall();

        int qty = 4;
        AddItemsToList(ref ItemsList, swordStats, qty);
        for (int i = 0; i < qty; i++)
        {
            var item = ItemsList[i];
            ItemPlacementHelpers.TryAutoPlaceToContainer(backpackContainer, item);
        }

        int countedQuantity = (int)partyManager.CountItemInParty(swordId);
        var character = partyManager.CurrentParty.Characters.ElementAt(0).Value;
        bool swordEquipped = false;
        foreach(var slot in character.EquipmentSlots.Values)
        {
            if (slot.EquippedItem is not null && slot.EquippedItem.Id.Equals(swordId))
                swordEquipped = true;
        }
        Assert.IsTrue(swordEquipped);
        Assert.AreEqual(expected: qty + 1, actual: countedQuantity);
    }

    [UnityTest]
    public IEnumerator CountMissingItemInPartySuccess()
    {
        CommonInstall();

        yield return LoadScene(sceneName);

        CommonPostSceneLoadInstall();

        int qty = 4;
        AddItemsToList(ref ItemsList, swordStats, qty);
        for (int i = 0; i < qty; i++)
        {
            var item = ItemsList[i];
            ItemPlacementHelpers.TryAutoPlaceToContainer(backpackContainer, item);
        }
        int countedQuantity = (int)partyManager.CountItemInParty("rope");
        Assert.AreEqual(expected: 0, actual: countedQuantity);
    }
}
