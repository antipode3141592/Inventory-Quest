using Data.Penalties;
using InventoryQuest.Health;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class PenaltyManager : MonoBehaviour, IPenaltyManager
    {
        IPartyManager _partyManager;
        IHealthManager _healthManager;
        IGameManager _gameManager;
        IQuestManager _questManager;

        readonly Queue<IPenaltyStats> penalties = new();

        public event EventHandler OnPenaltyProcessStart;
        public event EventHandler OnPenaltyProcessComplete;

        [Inject]
        public void Init(IPartyManager partyManager, IHealthManager healthManager, IGameManager gameManager, IQuestManager questManager)
        {
            _partyManager = partyManager;
            _healthManager = healthManager;
            _gameManager = gameManager;
            _questManager = questManager;
        }

        void Start()
        {
            _gameManager.OnGameOver += OnGameOverHandler;
        }

        void OnGameOverHandler(object sender, EventArgs e)
        {
            if (penalties.Count == 0) return;
            while (penalties.Count > 0)
            {
                penalties.Dequeue();
            }
        }

        public void EnqueuePenalty(IPenaltyStats penaltyStats)
        {
            penalties.Enqueue(penaltyStats);
        }

        public bool ProcessPenalties()
        {
            if (penalties.Count == 0) return false;
            while(penalties.Count > 0) {
                ProcessPenalty(penalties.Dequeue());
            }
            return true;
        }

        void ProcessPenalty(IPenaltyStats penalty)
        {
            DamagePenaltyStats damageStats = penalty as DamagePenaltyStats;
            if (damageStats is not null)
            {
                if (_partyManager.CurrentParty.Characters.Count == 0) return;
                Debug.Log($"Processing DamagePenalty with {damageStats.DamageAmount} pts of {damageStats.DamageType} type damage");
                QuestLog.Log($"Party takes {damageStats.DamageAmount} points of {damageStats.DamageType} damage!");
                foreach (var character in _partyManager.CurrentParty.Characters)
                {
                    Debug.Log($"...{damageStats.DamageAmount} {damageStats.DamageType} damage to {character.Value.DisplayName}");
                    _healthManager.DealDamage(character.Value, damageStats.DamageAmount, damageStats.DamageType);
                }
            }
            ItemLossPenaltyStats itemLoss = penalty as ItemLossPenaltyStats;
            if (itemLoss is not null)
            {
                Debug.Log($"Processing Item Loss Penalty: remove x{itemLoss.QuantityToLose} {itemLoss.ItemToLose}");
                _questManager.RemoveItemFromPartyInventory(itemLoss.ItemToLose.Id, itemLoss.QuantityToLose);
            }
        }
    }
}
