using Data;
using Data.Characters;
using Data.Items;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using Zenject;

namespace InventoryQuest.Testing
{
    public class TestContainer: SceneTestFixture
    {
        readonly string sceneName = "Test_Items";

        IItemDataSource itemDataSource;
        //container
        IItem backpack;
        IItem smallBox;

        IItemStats backpackStats;
        IItemStats smallBoxStats;
        //test items
        IItem MyItem;
        List<IItem> ItemsList;

        IItemStats AppleStats;
        IItemStats IngotStats;
        IItemStats SwordStats;

        void CommonInstall()
        {
            ItemsList = new List<IItem>();
        }

        void CommonPostSceneLoadInstall()
        {
            itemDataSource = SceneContainer.Resolve<IItemDataSource>();

            AppleStats = itemDataSource.GetById("apple_fuji");
            IngotStats = itemDataSource.GetById("ingot_common");
            SwordStats = itemDataSource.GetById("basic_sword_1");
            backpackStats = itemDataSource.GetById("adventure_backpack");
            smallBoxStats = itemDataSource.GetById("small_box");

            backpack = ItemFactory.GetItem(itemStats: backpackStats);
            smallBox = ItemFactory.GetItem(itemStats: smallBoxStats);
        }

        void AddItemsToList(ref List<IItem> itemList, IItemStats stats, int qty = 1)
        {
            for (int i = 0; i < qty; i++)
                itemList.Add(ItemFactory.GetItem(itemStats: stats));
        }

        [UnityTest]
        public IEnumerator NewContainerIsEmpty()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            Assert.IsTrue((smallBox.Components[typeof(IContainer)] as IContainer).IsEmpty);
        }

        [UnityTest]
        public IEnumerator NewEquipableContainerIsEmpty()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            Assert.IsTrue((backpack.Components[typeof(IContainer)] as IContainer).IsEmpty);
        }

        [UnityTest]
        public IEnumerator PlaceAtValidTarget()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();
            
            MyItem = ItemFactory.GetItem(itemStats: AppleStats);
            var backpackContainer = backpack.Components[typeof(IContainer)] as IContainer;
            float initialWeight = backpackContainer.InitialWeight + MyItem.Stats.Weight;
            backpackContainer.TryPlace(ref MyItem, new Coor(0, 1));
            Assert.AreEqual(expected: initialWeight, actual: backpackContainer.Weight);
        }

        [UnityTest]
        public IEnumerator PlaceSeveralItemsAtValidTargets()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            int qty = 4;
            AddItemsToList(ref ItemsList, AppleStats, qty);
            float initialWeight = (backpack.Components[typeof(IContainer)] as IContainer).InitialWeight;
            float targetWeight = initialWeight + (AppleStats.Weight * (float)qty);
            for (int i = 0; i < qty; i++)
            {
                var item = ItemsList[i];
                (backpack.Components[typeof(IContainer)] as IContainer).TryPlace(ref item, new Coor(0, 1 + i));
            }
            Assert.AreEqual(expected: targetWeight, actual: (backpack.Components[typeof(IContainer)] as IContainer).Weight);
        }

        [UnityTest]
        public IEnumerator FailPlaceAtOutOfBoundsTarget()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            MyItem = ItemFactory.GetItem(itemStats: AppleStats);
            Assert.IsFalse((backpack.Components[typeof(IContainer)] as IContainer).TryPlace(ref MyItem, new Coor(100, 100)));
        }

        [UnityTest]
        public IEnumerator FailPlaceAtOccupiedTarget()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();
            AddItemsToList(ref ItemsList, SwordStats, 2);
            var container = backpack.Components[typeof(IContainer)] as IContainer;
            var item = ItemsList[0];
            var item2 = ItemsList[1];
            container.TryPlace(ref item, new Coor(0, 1));
            Assert.IsFalse(container.TryPlace(ref item2, new Coor(0, 1)));
        }

        [UnityTest]
        public IEnumerator FailTakeAtOutOfBoundsTarget()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            Assert.IsFalse((backpack.Components[typeof(IContainer)] as IContainer).TryTake(out _, new Coor(100,100)));
        }

        [UnityTest]
        public IEnumerator PlaceAndTakeItem()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            var coordinate = new Coor(r: 0, c: 1);

            MyItem = ItemFactory.GetItem(itemStats: AppleStats);
            (backpack.Components[typeof(IContainer)] as IContainer).TryPlace(ref MyItem, coordinate);
            Assert.IsTrue((backpack.Components[typeof(IContainer)] as IContainer).TryTake(out _, coordinate));
            Assert.IsTrue((backpack.Components[typeof(IContainer)] as IContainer).IsEmpty);
        }

        [UnityTest]
        public IEnumerator PlaceSeveralItems()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            int qty = 4;
            AddItemsToList(ref ItemsList, AppleStats, qty);
            float initialWeight = (backpack.Components[typeof(IContainer)] as IContainer).InitialWeight;
            float targetWeight = initialWeight + (ItemsList[0].Stats.Weight * (float)qty);
            for (int i = 0; i < qty; i++)
            {
                var item = ItemsList[i];
                (backpack.Components[typeof(IContainer)] as IContainer).TryPlace(ref item, new Coor(r: 1, c: i));
            }
            Assert.AreEqual(expected: targetWeight, actual: (backpack.Components[typeof(IContainer)] as IContainer).Weight);
        }

        [UnityTest]
        public IEnumerator PlaceAndTakeSeveralItems()
        {
            CommonInstall();

            yield return LoadScene(sceneName);

            CommonPostSceneLoadInstall();

            int qty = 3;
            AddItemsToList(ref ItemsList, AppleStats, qty);
            float initialWeight = (backpack.Components[typeof(IContainer)] as IContainer).InitialWeight;
            for (int i = 1; i < qty; i++)
            {
                var item = ItemsList[i];
                (backpack.Components[typeof(IContainer)] as IContainer).TryPlace(ref item, new Coor(r: i, c: 0));
                (backpack.Components[typeof(IContainer)] as IContainer).TryTake(out _, new Coor(r: i, c: 0));
            }
            Assert.AreEqual(expected: initialWeight, actual: (backpack.Components[typeof(IContainer)] as IContainer).Weight);
            Assert.IsTrue((backpack.Components[typeof(IContainer)] as IContainer).IsEmpty);
            
        }



        #region Matching/Stacking

        //[UnityTest]
        //public void FindMatchingNeighboorsSuccess()
        //{
        //    int qty = 3;
        //    CreateStackableItems(StackableItemStats, qty);
        //    for (int i = 0; i < qty; i++)
        //    {
        //        Backpack.TryPlace(StackableItems[i], new Coor(r: i, c: 0 ));
        //    }
        //    HashSet<string> matchingNeighboors = new();
        //    if (Backpack.MatchingNeighboors(StackableItems[0], matchingNeighboors))
        //    {
        //        Debug.Log($"there are {matchingNeighboors.Count} matching neighboors to item with guid {StackableItems[0].GuId}");
        //        Assert.IsTrue(matchingNeighboors.Count > 0);
        //        return;
        //    }
        //    else
        //        Assert.Fail();
        //}

        //[UnityTest]
        //public void FindMatchingNeighboorsFail()
        //{
        //    int qty = 3;
        //    CreateStackableItems(StackableItemStats, qty);
        //    for (int i = 0; i < qty; i++)
        //    {
                
        //        Backpack.TryPlace(StackableItems[i], new Coor(r: 0, c: i * 4));
        //    }
        //    HashSet<string> matchingNeighboors = new();
        //    if (!Backpack.MatchingNeighboors(StackableItems[0], matchingNeighboors))
        //        Assert.Pass();
        //    else
        //        Assert.Fail();
        //}

        //[UnityTest]
        //public void ItemMaxStackSizeMetEventSuccess()
        //{
        //    int qty = 4;
        //    CreateStackableItems(StackableItemStats, qty);
        //    bool wasCalled = false;
        //    Backpack.OnStackComplete += (sender, e) => wasCalled = true;

        //    for (int i = 0; i < qty; i++)
        //    {
        //        Backpack.TryPlace(StackableItems[i], new Coor(r: 0, c: i));
        //    }
        //    Assert.IsTrue(wasCalled);
        //}

        //[Test]
        //public void ItemMatchingItemsEventSuccess()
        //{
        //    int qty = 2;
        //    CreateStackableItems(StackableItemStats, qty);
        //    bool wasCalled = false;
        //    Backpack.OnMatchingItems += (sender, e) => wasCalled = true;

        //    for (int i = 0; i < qty; i++)
        //    {
        //        Backpack.TryPlace(StackableItems[i], new Coor(r: 0, c: i));
        //    }
        //    Assert.IsTrue(wasCalled);
        //}

        //[Test]
        //public void ItemMaxStackSizeExceededEventSuccess()
        //{
        //    int qty = 7;
        //    CreateStackableItems(StackableItemStats, qty);

        //    Backpack.OnStackComplete += (sender, e) => Assert.IsTrue(e.Count == qty);

        //    Backpack.TryPlace(StackableItems[0], new Coor(r: 0, c: 0));
        //    Backpack.TryPlace(StackableItems[1], new Coor(r: 0, c: 1));
        //    Backpack.TryPlace(StackableItems[2], new Coor(r: 0, c: 2));

        //    Backpack.TryPlace(StackableItems[3], new Coor(r: 0, c: 4));
        //    Backpack.TryPlace(StackableItems[4], new Coor(r: 0, c: 5));
        //    Backpack.TryPlace(StackableItems[5], new Coor(r: 0, c: 6));

        //    Backpack.TryPlace(StackableItems[6], new Coor(r: 0, c: 3));
            
        //}

        //[Test]
        //public void LootPileHoldsItems()
        //{
        //    for (int i = 0; i < BasicItems.Count; i++)
        //        LootPile.TryPlace(BasicItems[i], new(0, i));

        //    Assert.IsTrue(LootPile.Contents.Count == BasicItems.Count);
        //}

        //[Test]
        //public void StackWeightCorrect()
        //{
        //    int qty = 4;
        //    CreateStackableItems(StackableItemStats, qty);
        //    float targetWeight = StackableItemStats.Weight * (float)qty;
        //    for (int i = 0; i < qty; i++)
        //        Backpack.TryPlace(StackableItems[i], new Coor(r: 0, c: i));
        //    Debug.Log($"contained weight: {Backpack.ContainedWeight}, target weight: {targetWeight}");
        //    int _qty = Backpack.Contents[Backpack.Contents.Keys.First()].Item.Quantity;
        //    Debug.Log($"qty: {_qty}, targetQty: {qty}");

        //    Assert.IsTrue( _qty == qty);
        //    Assert.IsTrue(Backpack.ContainedWeight == targetWeight);
        //}

        #endregion
    }
}
