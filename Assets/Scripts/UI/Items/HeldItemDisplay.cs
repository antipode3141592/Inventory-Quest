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
        IGameManager _gameManager;

        [SerializeField] Image itemImage;
        [SerializeField] TextMeshProUGUI ItemNameText;
        [SerializeField] TextMeshProUGUI ItemDescriptionText;
        [SerializeField] TextMeshProUGUI ItemRarityText;
        [SerializeField] TextMeshProUGUI ItemValueText;
        [SerializeField] TextMeshProUGUI ItemWeightText;
        [SerializeField] TextMeshProUGUI ShapeTypeText;
        [SerializeField] List<TextMeshProUGUI> ItemModifiersTexts;

        [Inject]
        public void Init(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        void Awake()
        {
            _gameManager.OnItemHeld += OnItemHeldHandler;
            _gameManager.OnItemPlaced += OnItemPlacedHandler;
        }

        private void Start()
        {
            ClearDisplay();
        }

        void OnItemPlacedHandler(object sender, EventArgs e)
        {
            //clear display
            ClearDisplay();
        }

        

        void OnItemHeldHandler(object sender, EventArgs e)
        {
            //populate display
            DisplayItemDetails(_gameManager.HoldingItem);
        }

        void DisplayItemDetails(IItem item)
        {
            itemImage.sprite = item.Sprite; 
            ItemNameText.text = item.Stats.Id;
            ItemDescriptionText.text = item.Stats.Description;
            ItemRarityText.text = item.Stats.Rarity.ToString();
            ItemValueText.text = $"{item.Stats.GoldValue:#,###.#} gp";
            ItemWeightText.text = $"{item.Stats.Weight:#,###.#} kg";
            ShapeTypeText.text = item.Stats.ShapeType.ToString();
            var equipableItem = item as EquipableItem;

            for(int i = 0; i < ItemModifiersTexts.Count; i++)
            {
                if (equipableItem is not null && equipableItem.Modifiers.Count > i)
                { 
                    ItemModifiersTexts[i].text = equipableItem.Modifiers[i].ToString();
                }
                else
                {
                    ItemModifiersTexts[i].text = "";

                }
            }
        }

        void ClearDisplay()
        {
            itemImage.sprite = null;
            ItemNameText.text = "";
            ItemDescriptionText.text = "";
            ItemRarityText.text = "";
            ItemValueText.text = "";
            ItemWeightText.text = "";
            ShapeTypeText.text = "";
            foreach (var modText in ItemModifiersTexts)
            {
                modText.text = "";
            }
        }
    }
}
