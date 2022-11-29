using Data.Items;
using InventoryQuest.Managers;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class HeldItemDisplay : MonoBehaviour
    {
        IInputManager _inputManager;

        [SerializeField] Image backgroundImage;
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI ItemNameText;
        [SerializeField] TextMeshProUGUI ItemDescriptionText;
        [SerializeField] TextMeshProUGUI ItemRarityText;
        [SerializeField] TextMeshProUGUI ItemValueText;
        [SerializeField] TextMeshProUGUI ItemWeightText;
        [SerializeField] List<TextMeshProUGUI> ItemModifiersTexts;
        [SerializeField] TextMeshProUGUI QuantityText;

        [Inject]
        public void Init(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        void Start()
        {
            _inputManager.OnItemHeld += OnItemHeldHandler;
            _inputManager.OnItemPlaced += OnItemPlacedHandler;
            backgroundImage.gameObject.SetActive(false);
        }

        void OnItemPlacedHandler(object sender, EventArgs e)
        {
            backgroundImage.gameObject.SetActive(false);
        }

        void OnItemHeldHandler(object sender, EventArgs e)
        {
            backgroundImage.gameObject.SetActive(true);
            //populate display
            DisplayItemDetails(_inputManager.HoldingItem);
        }

        void DisplayItemDetails(IItem item)
        {
            itemImage.sprite = item.Sprite; 
            ItemNameText.text = item.Stats.Id;
            ItemDescriptionText.text = item.Stats.Description;
            ItemRarityText.text = item.Stats.Rarity.ToString();
            ItemValueText.text = $"{item.Stats.GoldValue:#,###.#} gp";
            ItemWeightText.text = $"{item.Weight:#,###.#} kg";
            var equipable = item.Components.ContainsKey(typeof(IEquipable)) ? item.Components[typeof(IEquipable)] as IEquipable : null;
            if (equipable is not null)
                for (int i = 0; i < ItemModifiersTexts.Count; i++)
                {
                    if (equipable.Modifiers is not null && equipable.Modifiers.Count > i)
                    {
                        ItemModifiersTexts[i].text = equipable.Modifiers[i].ToString();
                    }
                    else
                    {
                        ItemModifiersTexts[i].text = "";

                    }
                }
            else
                for (int i = 0; i < ItemModifiersTexts.Count; i++)
                    ItemModifiersTexts[i].text = "";

            QuantityText.text = item is not IStackable ? "" : $"Qty: {item.Quantity}";

        }
    }
}
