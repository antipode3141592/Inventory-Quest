using Data.Characters;
using Data.Items;
using InventoryQuest.Testing.Stubs;
using NUnit.Framework;

namespace InventoryQuest.Testing
{
    public class TestEquipableCreation
    {
        readonly string equipableItemId = "basic_sword_15";

        IItemDataSource itemDataSource;
        IItemStats itemStats;
        IItem item;

        ICharacterDataSource characterDataSource;
        ICharacterStats characterStats;
        ICharacter character;

        [SetUp]
        public void SetUp()
        {
            itemDataSource = new ItemDataSourceTest();
            itemStats = itemDataSource.GetById(equipableItemId);
            item = ItemFactory.GetItem(itemStats);

            characterDataSource = new CharacterDataSourceTest();
            characterStats = characterDataSource.GetById("player");
            character = CharacterFactory.GetCharacter(baseStats: characterStats, startingEquipment: new IItem[] { item });

        }

        [TearDown]
        public void TearDown()
        {
            itemStats = null;
            item = null;
        }

        [Test]
        public void EquipableItemCreationSuccess()
        {
            Assert.IsTrue(item is not null);
        }

        [Test]
        public void EquipableComponentExists()
        {
            if (item.Components.Count == 0)
                Assert.Fail($"No components found!");
            Assert.IsNotNull(item.Components[typeof(IEquipable)] as IEquipable);
        }

        [Test]
        public void EquipmentSlotTypeCorrect()
        {
            var slotType = (item.Components[typeof(IEquipable)] as IEquipable).SlotType;
            IEquipableStats equipableStats = null;
            EquipmentSlotType itemStatSlotType = EquipmentSlotType.Ring;
            foreach (var component in itemStats.Components) {
               var _equipableStats = component as IEquipableStats;
                if (_equipableStats is not null)
                {
                    itemStatSlotType = _equipableStats.SlotType;
                    equipableStats = _equipableStats;
                }
            }
            if (equipableStats is null)
                Assert.Fail($"no equipable stats found");

            Assert.IsTrue(slotType == itemStatSlotType, message: $"{slotType}");
        }

        [Test]
        public void StartingEquipmentEquipSuccess()
        {
            foreach(var slot in character.EquipmentSlots)
            {
                if (slot.Value is not null && slot.Value.EquippedItem.Id == itemStats.Id)
                    Assert.Pass($"Item {itemStats.Id} equipped!");
            }
            Assert.Fail($"{itemStats.Id} is not equipped!");
        }

    }
}
