using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    public abstract class EncounterStatsSO : ScriptableObject, IEncounterStats
    {
        [SerializeField] string id;
        [SerializeField] string _name;
        [SerializeField, TextArea(1, 5)] string description;
        [SerializeField] int experience;
        [SerializeField, TextArea(1, 5)] string successMessage;
        [SerializeField, TextArea(1, 5)] string failureMessage;
        [SerializeField] List<string> rewardIds;
        [SerializeField] List<string> penaltyIds;

        public string Id => id;
        public string Name => _name;
        public string Description => description;
        public int Experience => experience;
        public string SuccessMessage => successMessage;
        public string FailureMessage => failureMessage;
        public List<string> RewardIds => rewardIds;
        public List<string> PenaltyIds => penaltyIds;
    }
}
