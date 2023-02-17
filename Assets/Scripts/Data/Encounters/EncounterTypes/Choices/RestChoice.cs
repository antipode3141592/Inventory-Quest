using Data.Characters;
using Data.Penalties;
using Data.Rewards;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    public class RestChoice : IChoice
    {
        [SerializeField] string description;

        public string Description => description;
        public string SuccessMessage => "";
        public string FailureMessage => "";
        public int Experience => 0;
        public List<IRewardStats> Rewards { get; } = new();
        public List<IPenaltyStats> Penalties { get; } = new();

        public bool Resolve(Party party)
        {
            foreach(var character in party.Characters.Values)
            {
                character.CurrentHealth = character.MaximumHealth;
                character.CurrentMagicPool = character.MaximumMagicPool;
            }
            return true;
        }
    }
}
