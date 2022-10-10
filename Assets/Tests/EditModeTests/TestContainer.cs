using Data;
using Data.Items;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
        List<IItem> BasicItems;
        List<StackableItem> StackableItems;

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
            
            BasicItems = new List<IItem>();
            StackableItems = new List<StackableItem>();
            Backpack = (ContainerBase)ItemFactory.GetItem(stats: backpackStats);
            LootPile = (ContainerBase)ItemFactory.GetItem(stats: lootPileStats);
        }

        void CreateStandardItems(IItemStats stats, int qty = 1)
        {
            for (int i = 0; i < qty; i++)
                BasicItems.Add((IItem)ItemFactory.GetItem(stats: BasicItemStats));
        }

        void CreateStackableItems(IItemStats stats, int qty = 2)
        {
            for (int i = 0; i < qty; i++)
                StackableItems.Add((StackableItem)ItemFactory.GetItem(stats: StackableItemStats));
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
            MyItem = (Item)ItemFactory.GetItem(stats: BasicItemStats);
            float initialWeight = Backpack.InitialWeight;
            Backpack.TryPlace(MyItem, new Coor(0, 0));
            Assert.AreEqual(expected: initialWeight + MyItem.Stats.Weight, actual: (Backpack as ContainerBase).Weight);
        }

        [Test]
        public void PlaceSeveralItemsAtValidTargets()
        {
            int qty = 4;
            CreateStandardItems(BasicItemStats, qty);
            float initialWeight = Backpack.InitialWeight;
            float targetWeight = initialWeight + (BasicItemStats.Weight * (float)qty);
            for (int i = 0; i < qty; i++)
            {
                Backpack.TryPlace(BasicItems[i], new Coor(0, 0 + i));
            }
            Assert.AreEqual(expected: targetWeight, actual: (Backpack as ContainerBase).Weight);
        }

        [Test]
        public void FailPlaceAtOutOfBoundsTarget()
        {
            MyItem = (Item)ItemFactory.GetItem(stats: BasicItemStats);
            CreateStandardItems(BasicItemStats, 1);
            Assert.IsFalse(Backpack.TryPlace(MyItem, new Coor(Backpack.Dimensions.row+1, 0)));
        }

        [Test]
        public void FailPlaceAtOccupiedTarget()
        {
            MyItem = (Item)ItemFactory.GetItem(stats: BasicItemStats);
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
            MyItem = (Item)ItemFactory.GetItem(stats: BasicItemStats);
            Backpack.TryPlace(MyItem, new Coor(r: 0, c: 0));
            Assert.IsTrue(Backpack.TryTake(out _, new Coor(r:0, c:0)));
            Assert.IsTrue(Backpack.IsEmpty);
        }

        [Test]
        public void PlaceSeveralItems()
        {
            int qty = 4;
            CreateStandardItems(BasicItemStats, qty);
            float initialWeight = Backpack.InitialWeight;
            float targetWeight = initialWeight + (BasicItems[0].Stats.Weight * (float)qty);
            for (int i = 0; i < qty; i++)
            {
                Backpack.TryPlace(BasicItems[i], new Coor(r: 0, c: i));
            }
            Assert.AreEqual(expected: targetWeight, actual: (Backpack as IItem).Weight);
        }

        [Test]
        public void PlaceAndTakeSeveralItems()
        {
            int qty = 3;
            CreateStandardItems(BasicItemStats, qty);
            float initialWeight = Backpack.InitialWeight;
            for (int i = 0; i < 3; i++)
            {
                Backpack.TryPlace(BasicItems[i], new Coor(r: i, c: 0));
                Backpack.TryTake(out _, new Coor(r: i, c: 0));
            }
            Assert.AreEqual(expected: initialWeight, actual: (Backpack as IItem).Weight);
            Assert.IsTrue(Backpack.IsEmpty);
            
        }

        [Test]
        public void FindMatchingNeighboorsSuccess()
        {
            int qty = 3;
            CreateStackableItems(StackableItemStats, qty);
            for (int i = 0; i < qty; i++)
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
            int qty = 3;
            CreateStackableItems(StackableItemStats, qty);
            for (int i = 0; i < qty; i++)
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
            int qty = 4;
            CreateStackableItems(StackableItemStats, qty);
            bool wasCalled = false;
            Backpack.OnStackComplete += (sender, e) => wasCalled = true;

            for (int i = 0; i < qty; i++)
            {
                Backpack.TryPlace(StackableItems[i], new Coor(r: 0, c: i));
            }
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ItemMatchingItemsEventSuccess()
        {
            int qty = 2;
            CreateStackableItems(StackableItemStats, qty);
            bool wasCalled = false;
            Backpack.OnMatchingItems += (sender, e) => wasCalled = true;

            for (int i = 0; i < qty; i++)
            {
                Backpack.TryPlace(StackableItems[i], new Coor(r: 0, c: i));
            }
            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ItemMaxStackSizeExceededEventSuccess()
        {
            int qty = 7;
            CreateStackableItems(StackableItemStats, qty);

            Backpack.OnStackComplete += (sender, e) => Assert.IsTrue(e.Count == qty);

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

        [Test]
        public void StackWeightCorrect()
        {
            int qty = 4;
            CreateStackableItems(StackableItemStats, qty);
            float targetWeight = StackableItemStats.Weight * (float)qty;
            for (int i = 0; i < qty; i++)
                Backpack.TryPlace(StackableItems[i], new Coor(r: 0, c: i));
            Debug.Log($"contained weight: {Backpack.ContainedWeight}, target weight: {targetWeight}");
            int _qty = Backpack.Contents[Backpack.Contents.Keys.First()].Item.Quantity;
            Debug.Log($"qty: {_qty}, targetQty: {qty}");

            Assert.IsTrue( _qty == qty);
            Assert.IsTrue(Backpack.ContainedWeight == targetWeight);
        }
    }
}
