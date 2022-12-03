using Data.Characters;
using Data.Items;
using InventoryQuest.Managers;
using Sirenix.OdinInspector;
using Zenject;

namespace Assets.Tests.PlayModeTests.ManualDrivers
{
    class HarvestDriver: SerializedMonoBehaviour
    {
        IHarvestManager _harvestManager;
        ICharacterDataSource _characterDataSource;
        IItemDataSource _itemDataSource;
        IPartyManager _partyManager;

        [Inject]
        public void Init(IHarvestManager harvestManager, ICharacterDataSource characterDataSource, IItemDataSource itemDataSource, IPartyManager partyManager)
        {
            _harvestManager = harvestManager;
            _characterDataSource = characterDataSource;
            _itemDataSource = itemDataSource;
            _partyManager = partyManager;
        }
        
        [Button]
        public void BeginHarvest()
        {
            var wagonStats = _characterDataSource.GetById("wagon");
            var wagonContainerStats = _itemDataSource.GetById("wagon_standard");

            var wagonContainer = ItemFactory.GetItem(itemStats: wagonContainerStats);
            var wagon = CharacterFactory.GetCharacter(baseStats: wagonStats, startingEquipment: new IItem[] { wagonContainer });

            _partyManager.AddCharacterToParty(wagon);

            _harvestManager.BeginHarvest(harvestType: HarvestTypes.Forest);
        }

    }
}
