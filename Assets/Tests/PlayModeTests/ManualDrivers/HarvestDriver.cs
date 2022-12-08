using Data.Characters;
using InventoryQuest.Managers;
using Sirenix.OdinInspector;
using Zenject;

namespace Assets.Tests.PlayModeTests.ManualDrivers
{
    class HarvestDriver: SerializedMonoBehaviour
    {
        IHarvestManager _harvestManager;
        ICharacterDataSource _characterDataSource;
        IPartyManager _partyManager;

        [Inject]
        public void Init(IHarvestManager harvestManager, ICharacterDataSource characterDataSource, IPartyManager partyManager)
        {
            _harvestManager = harvestManager;
            _characterDataSource = characterDataSource;
            _partyManager = partyManager;
        }
        
        [Button]
        public void BeginHarvest()
        {
            var wagonStats = _characterDataSource.GetById("wagon");
            _partyManager.AddCharacterToParty(wagonStats);
            _harvestManager.BeginHarvest(harvestType: HarvestTypes.Forest);
        }

    }
}
