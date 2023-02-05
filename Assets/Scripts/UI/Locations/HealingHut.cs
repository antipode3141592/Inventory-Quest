using InventoryQuest.Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class HealingHut : MonoBehaviour
    {
        IPartyManager _partymanager;

        [SerializeField] double goldCost = 5;
        [SerializeField] TextMeshProUGUI descriptionText;

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
            if (_partymanager.CountItemInCharacterInventories("gold") < goldCost)
                return;
            _partymanager.CurrentParty.RemoveItemFromPartyInventory("gold", goldCost);
            foreach(var character in _partymanager.CurrentParty.Characters.Values)
            {
                //restore health and magic
                character.CurrentHealth = character.MaximumHealth;
                character.CurrentMagicPool = character.MaximumMagicPool;
            }
        }
    }
}