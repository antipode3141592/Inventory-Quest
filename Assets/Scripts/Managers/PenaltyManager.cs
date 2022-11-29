using Data.Characters;
using Data.Penalties;
using InventoryQuest.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class PenaltyManager : MonoBehaviour, IPenaltyManager
    {
        IPartyManager _partyManager;
        IHealthManager _healthManager;

        Queue<IPenaltyStats> penalties = new();

        public event EventHandler OnPenaltyProcessStart;
        public event EventHandler OnPenaltyProcessComplete;

        [Inject]
        public void Init(IPartyManager partyManager, IHealthManager healthManager)
        {
            _partyManager = partyManager;
            _healthManager = healthManager;
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
                Debug.Log($"Processing DamagePenalty with {damageStats.DamageAmount} pts of {damageStats.DamageType} type damage");
                foreach (var character in _partyManager.CurrentParty.Characters)
                {
                    _healthManager.DealDamage(character.Value, damageStats.DamageAmount, damageStats.DamageType);
                }
        }
    }
}
