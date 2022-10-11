using InventoryQuest.Managers;
using UnityEngine;
using Zenject;

namespace InventoryQuest
{
    public class HarvestLocation: MonoBehaviour
    {
        [SerializeField] HarvestTypes _harvestType;

        IHarvestManager _harvestManager;

        [Inject]
        public void Init(IHarvestManager harvestManager)
        {
            _harvestManager = harvestManager;
        }

        void OnMouseUpAsButton()
        {
            _harvestManager.BeginHarvest(_harvestType);
        }
    }
}
