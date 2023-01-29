using Data.Items;
using Data.Items.Components;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class ItemDetailDisplay : MonoBehaviour
    {
        [SerializeField] Image backgroundImage;
        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI ItemNameText;
        [SerializeField] TextMeshProUGUI ItemDescriptionText;
        [SerializeField] TextMeshProUGUI ItemRarityText;
        [SerializeField] TextMeshProUGUI ItemValueText;
        [SerializeField] TextMeshProUGUI ItemWeightText;
        [SerializeField] List<TextMeshProUGUI> ItemModifiersTexts;
        [SerializeField] TextMeshProUGUI QuantityText;
        [SerializeField] TextMeshProUGUI TypesText;

        public void OnItemPlacedHandler(object sender, EventArgs e)
        {
            ClearItemDetails();
        }

        public void OnItemHeldHandler(object sender, IItem item)
        {
            DisplayItemDetails(item);
        }

        public void DisplayItemDetails(IItem item)
        {
            if (item.Quantity == 0)
            {
                ClearItemDetails();
                return;
            }
            itemImage.sprite = item.Sprite;
            itemImage.color = Color.white;
            ItemNameText.text = item.DisplayName;
            ItemDescriptionText.text = item.Stats.Description;
            ItemRarityText.text = item.Stats.Rarity.ToString();
            ItemValueText.text = $"{item.Value:#,###.#} gp";
            ItemWeightText.text = $"{item.Weight:#,###.#} kg";

            for (int i = 0; i < ItemModifiersTexts.Count; i++)
                ItemModifiersTexts[i].text = "";

            var equipable = item.Components.ContainsKey(typeof(IEquipable)) ? item.Components[typeof(IEquipable)] as IEquipable : null;
            var usable = item.Components.ContainsKey(typeof(IUsable)) ? item.Components[typeof(IUsable)] as IUsable : null;
            if (equipable is not null)
                if (equipable.Modifiers is not null && equipable.Modifiers.Count > 0)
                    for (int i = 0; i < equipable.Modifiers.Count; i++)
                        ItemModifiersTexts[i].text = equipable.Modifiers[i].ToString();
            if (usable is not null)
            {
                if (usable is Edible edible)
                {
                    ItemModifiersTexts[0].text = edible.ToString();
                }
                else if (usable is EncounterLengthEffect encounterLengthEffect)
                {
                    for(int i = 0; i < encounterLengthEffect.EncounterLengthEffectStats.Modifiers.Count; i++)
                    {
                        ItemModifiersTexts[i].text = $"{encounterLengthEffect.EncounterLengthEffectStats.Modifiers[i]} when used";
                    }
                }
            }

            QuantityText.text = $"Qty: {item.Quantity}";
            List<string> typeList = new();
            foreach (var component in item.Components)
            {
                if (component.Key == typeof(IUsable))
                    typeList.Add("Consumable");
                if (component.Key == typeof(IEquipable))
                    typeList.Add("Equipment");
            }
            TypesText.text = string.Join(", ", typeList).TrimEnd(',', ' ');

        }

        public void ClearItemDetails()
        {
            itemImage.sprite = null;
            itemImage.color = Color.clear;
            ItemNameText.text = "";
            ItemDescriptionText.text = "";
            ItemRarityText.text = "";
            ItemValueText.text = "";
            ItemWeightText.text = "";
            for (int i = 0; i < ItemModifiersTexts.Count; i++)
                ItemModifiersTexts[i].text = "";
            QuantityText.text = "";
            TypesText.text = "";
        }
    }
}
