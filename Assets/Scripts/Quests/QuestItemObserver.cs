using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using PixelCrushers.DialogueSystem;
using InventoryQuest.Managers;
using System;

namespace InventoryQuest
{
    public class QuestItemObserver : MonoBehaviour
    {
        IPartyManager _partyManager;
        IQuestManager _questManager;

        readonly List<QuestItemCounter> _questItemCounterList = new();

        [Inject]
        public void Init(IPartyManager partyManager, IQuestManager questManager)
        {
            _partyManager = partyManager;
            _questManager = questManager;
        }

        void Start()
        {
            _partyManager.CurrentParty.OnPartyCompositionChanged += SetCharacterSubscriptions;
            _partyManager.CurrentParty.OnPartyMemberStatsUpdated += OnPartyStatsUpdated;

            Lua.RegisterFunction("StartObservingItem", this, SymbolExtensions.GetMethodInfo(() => StartObservingItem(string.Empty, 0)));
        }

        void OnPartyStatsUpdated(object sender, EventArgs e)
        {
            foreach(var listItem in _questItemCounterList)
            {
                int quantity = (int)_questManager.CountItemInPartyInventory(listItem.ItemId);
                listItem.Quantity = quantity;
            }
        }

        public void StartObservingItem(string itemId, double targetQuantity)
        {
            if (_questItemCounterList.Find(x => x.ItemId == itemId) is null) return;
            int initialQty = (int)_questManager.CountItemInPartyInventory(itemId);
            _questItemCounterList.Add(new QuestItemCounter(itemId, initialQty, (int)targetQuantity));
        }

        public void EndObservingItem(string itemID)
        {
            var listItem = _questItemCounterList.Find(x => x.ItemId == itemID);
            if (listItem is null) return;
            _questItemCounterList.Remove(listItem);
        }

        void SetCharacterSubscriptions(object sender, EventArgs e)
        {

        }
    }

    public class QuestItemCounter
    {
        public QuestItemCounter(string itemId, int quantity, int targetQuantity)
        {
            ItemId = itemId;
            Quantity = quantity;
            TargetQuantity = targetQuantity;
        }

        public string ItemId { get; set; }
        public int Quantity { get; set; }
        public int TargetQuantity { get; set; }
    }
}