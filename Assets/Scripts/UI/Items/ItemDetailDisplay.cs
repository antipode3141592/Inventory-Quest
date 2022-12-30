using Data.Items;
using InventoryQuest.Managers;
using InventoryQuest.UI.Menus;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class ItemDetailDisplay : MonoBehaviour, IOnMenuShow, IOnMenuHide
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

        void OnItemPlacedHandler(object sender, EventArgs e)
        {
            ClearItemDetails();
        }

        void OnItemHeldHandler(object sender, IItem item)
        {
            DisplayItemDetails(item);
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

            QuantityText.text = $"Qty: {item.Quantity}";

        }

        void ClearItemDetails()
        {
            itemImage.sprite = null;
            ItemNameText.text = "";
            ItemDescriptionText.text = "";
            ItemRarityText.text = "";
            ItemValueText.text = "";
            ItemWeightText.text = "";
            for (int i = 0; i < ItemModifiersTexts.Count; i++)
                ItemModifiersTexts[i].text = "";
            QuantityText.text = "";
        }

        public void OnShow()
        {
            _inputManager.ShowItemDetailsCommand += OnItemHeldHandler;
            _inputManager.HideItemDetailsCommand += OnItemPlacedHandler;
        }

        public void OnHide()
        {
            _inputManager.ShowItemDetailsCommand -= OnItemHeldHandler;
            _inputManager.HideItemDetailsCommand -= OnItemPlacedHandler;
        }
    }
}
