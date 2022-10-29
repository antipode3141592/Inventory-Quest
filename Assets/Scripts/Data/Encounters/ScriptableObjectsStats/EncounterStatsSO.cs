using System.Collections.Generic;
using UnityEngine;
using Data.Rewards;
using Data.Penalties;
using Sirenix.OdinInspector;

namespace Data.Encounters
{
    public abstract class EncounterStatsSO : SerializedScriptableObject, IEncounterStats
    {
        [SerializeField] string id;
        [SerializeField] string _name;
        [SerializeField, TextArea(1, 5)] string description;
        [SerializeField] int experience;
        [SerializeField, TextArea(1, 5)] string successMessage;
        [SerializeField, TextArea(1, 5)] string failureMessage;
        [SerializeField] List<IRewardStats> rewards = new();
        [SerializeField] List<IPenaltyStats> penalties = new();

        public string Id => id;
        public string Name => _name;
        public string Description => description;
        public int Experience => experience;
        public string SuccessMessage => successMessage;
        public string FailureMessage => failureMessage;
        public List<IRewardStats> Rewards => rewards;
        public List<IPenaltyStats> Penalties => penalties;
    }
}
