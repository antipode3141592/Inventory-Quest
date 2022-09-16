using Data;
using Data.Items;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.Testing
{
    public class TestContainer
    {
        IItemDataSource dataSource;
        //container
        ContainerBase Backpack;
        ContainerBase LootPile;

        EquipableContainerStats backpackStats;
        ContainerStats lootPileStats;
        //test items
        Item MyItem;
        List<Item> BasicItems;
        List<StackableItem> StackableItems;

        int MyTotalItems = 4;
        ItemStats BasicItemStats;
        StackableItemStats StackableItemStats;


        [SetUp]
        public void Setup()
        {
            dataSource = new ItemDataSourceTest();
            BasicItemStats = (ItemStats)dataSource.GetItemStats("apple_fuji");
            StackableItemStats = (StackableItemStats)dataSource.GetItemStats("ingot_common");
            backpackStats = (EquipableContainerStats)dataSource.GetItemStats("adventure backpack");
            lootPileStats = (ContainerStats)dataSource.GetItemStats("loot_pile_small");
            MyItem = (Item)ItemFactory.GetItem(stats: BasicItemStats);
            BasicItems = new List<Item>();
            StackableItems = new List<StackableItem>();
            for (int i = 0; i < MyTotalItems; i++)
            {
                BasicItems.Add((Item)ItemFactory.GetItem(stats: BasicItemStats));
                StackableItems.Add((StackableItem)ItemFactory.GetItem(stats: StackableItemStats));
            }
            Backpack = (ContainerBase)ItemFactory.GetItem(stats: backpackStats);
            LootPile = (ContainerBase)ItemFactory.GetItem(stats: lootPileStats);
        }

        [TearDown]
        public void TearDown()
        {
            Backpack = null;
            MyItem = null;
            BasicItems = null;
            StackableItems = null;
        }

        [Test]
        public void ContainerSizeIsCorrect()
        {
            Assert.AreEqual(expected: backpackStats.ContainerSize, actual: Backpack.Dimensions);  
        }

        [Test]
        public void NewContainerIsEmpty()
        {
            Assert.IsTrue(Backpack.IsEmpty);
        }

        [Test]
        public void PlaceAtValidTarget()
        {
            float initialWeight = Backpack.InitialWeight;
            Backpack.TryPlace(MyItem, new Coor(0, 0));
            Assert.AreEqual(expected: initialWeight + MyItem.Stats.Weight, actual: (Backpack as ContainerBase).Weight);
        }

        [Test]
        public void PlaceSeveralItemsAtValidTargets()
        {
            float initialWeight = Backpack.InitialWeight;
            float targetWeight = initialWeight + (MyItem.Stats.Weight * MyTotalItems);
            for (int i = 0; i < MyTotalItems; i++)
            {
                Backpack.TryPlace(BasicItems[i], new Coor(0, 0 + i));
            }
            Assert.AreEqual(expected: targetWeight, actual: (Backpack as ContainerBase).Weight);
        }

        [Test]
        public void FailPlaceAtOutOfBoundsTarget()
        {
            Assert.IsFalse(Backpack.TryPlace(MyItem, new Coor(Backpack.Dimensions.row+1, 0)));
        }

        [Test]
        public void FailPlaceAtOccupiedTarget()
        {
            Backpack.Grid[0, 0].IsOccupied = true;
            Assert.IsFalse(Backpack.TryPlace(MyItem, new Coor(0, 0)));
        }

        [Test]
        public void FailTakeAtOutOfBoundsTarget()
        {
            Assert.IsFalse(Backpack.TryTake(out _, new Coor(r: Backpack.Dimensions.row + 1, c: 0)));
        }

        [Test]
        public void PlaceAndTakeItem()
        {
            Backpack.TryPlace(MyItem, new Coor(r: 0, c: 0));
            Assert.IsTrue(Backpack.TryTake(out _, new Coor(r:0, c:0)));
            Assert.IsTrue(Backpack.IsEmpty);
        }

        [Test]
        public void PlaceSeveralItems()
        {
            float initialWeight = Backpack.InitialWeight;
            float targetWeight = initialWeight + (BasicItems[0].Stats.Weight * (float)MyTotalItems);
            for (int i = 0; i < MyTotalItems; i++)
            {
                Backpack.TryPlace(BasicItems[i], new Coor(r: 0, c: i));
            }
            Assert.AreEqual(expected: targetWeight, actual: (Backpack as IItem).Weight);
        }

        [Test]
        public void PlaceAndTakeSeveralItems()
        {
            float initialWeight = Backpack.InitialWeight;
            float targetWeight = initialWeight + (StackableItems[0].Stats.Weight * (float)MyTotalItems);
            for (int i = 0; i < MyTotalItems-1; i++)
            {
                Backpack.TryPlace(StackableItems[i], new Coor(r: i, c: 0));
                Backpack.TryTake(out _, new Coor(r: i, c: 0));
            }
            Assert.AreEqual(expected: initialWeight, actual: (Backpack as IItem).Weight);
            Assert.IsTrue(Backpack.IsEmpty);
            
        }

        [Test]
        public void FindMatchingNeighboorsSuccess()
        {
            for (int i = 0; i < MyTotalItems - 1; i++)
            {
                Backpack.TryPlace(StackableItems[i], new Coor(r: i, c: 0 ));
            }
            HashSet<string> matchingNeighboors = new();
            if (Backpack.MatchingNeighboors(StackableItems[0], matchingNeighboors))
            {
                Debug.Log($"there are {matchingNeighboors.Count} matching neighboors to item with guid {StackableItems[0].GuId}");
                Assert.IsTrue(matchingNeighboors.Count > 0);
                return;
            }
            else
                Assert.Fail();
        }

        [Test]
        public void FindMatchingNeighboorsFail()
        {
            for (int i = 0; i < MyTotalItems - 1; i++)
            {
                
                Backpack.TryPlace(StackableItems[i], new Coor(r: 0, c: i * 4));
            }
            HashSet<string> matchingNeighboors = new();
            if (!Backpack.MatchingNeighboors(StackableItems[0], matchingNeighboors))
                Assert.Pass();
            else
                Assert.Fail();
        }

        [Test]
        public void ItemMaxStackSizeMetEventSuccess()
        {
            bool wasCalled = false;
            Backpack.OnStackComplete += (sender, e) => wasCalled = true;

            for (int i = 0; i < MyTotalItems; i++)
            {
                Backpack.TryPlace(StackableItems[i], new Coor(r: 0, c: i));
            }
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ItemMatchingItemsEventSuccess()
        {
            bool wasCalled = false;
            Backpack.OnMatchingItems += (sender, e) => wasCalled = true;

            for (int i = 0; i < 2; i++)
            {
                Backpack.TryPlace(StackableItems[i], new Coor(r: 0, c: i));
            }
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ItemMaxStackSizeExceededEventSuccess()
        {
            StackableItems.Add((StackableItem)ItemFactory.GetItem(stats: StackableItemStats));
            StackableItems.Add((StackableItem)ItemFactory.GetItem(stats: StackableItemStats));
            StackableItems.Add((StackableItem)ItemFactory.GetItem(stats: StackableItemStats));

            Backpack.OnStackComplete += (sender, e) => Assert.IsTrue(e.Count == 7);

            Backpack.TryPlace(StackableItems[0], new Coor(r: 0, c: 0));
            Backpack.TryPlace(StackableItems[1], new Coor(r: 0, c: 1));
            Backpack.TryPlace(StackableItems[2], new Coor(r: 0, c: 2));

            Backpack.TryPlace(StackableItems[3], new Coor(r: 0, c: 4));
            Backpack.TryPlace(StackableItems[4], new Coor(r: 0, c: 5));
            Backpack.TryPlace(StackableItems[5], new Coor(r: 0, c: 6));

            Backpack.TryPlace(StackableItems[6], new Coor(r: 0, c: 3));
            
        }

        [Test]
        public void LootPileHoldsItems()
        {
            for (int i = 0; i < BasicItems.Count; i++)
                LootPile.TryPlace(BasicItems[i], new(0, i));

            Assert.IsTrue(LootPile.Contents.Count == BasicItems.Count);
        }
    }
}
