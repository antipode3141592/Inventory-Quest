using InventoryQuest.Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Locations
{
    public class HealingHut : MonoBehaviour, IEnableable
    {
        IPartyManager _partymanager;

        [SerializeField] double goldCost = 5;
        [SerializeField] TextMeshProUGUI descriptionText;

        bool isAvailable = true;

        [Inject]
        public void Init(IPartyManager partyManager)
        {
            _partymanager = partyManager;
        }

        void Awake()
        {
            descriptionText.text = $"Pay {goldCost} gold to heal party.";
        }

        public void OnMouseUpAsButton()
        {
            if (!isAvailable || _partymanager.CountItemInCharacterInventories("gold") < goldCost)
                return;
            _partymanager.CurrentParty.RemoveItemFromPartyInventory("gold", goldCost);
            foreach(var character in _partymanager.CurrentParty.Characters.Values)
            {
                //restore health and magic
                character.CurrentHealth = character.MaximumHealth;
                character.CurrentMagicPool = character.MaximumMagicPool;
            }
        }

        public void Disable()
        {
            isAvailable = false;
        }

        public void Enable()
        {
            isAvailable = true;
        }
    }
}