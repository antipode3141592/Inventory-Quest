using Data.Characters;
using Data.Items;
using Data.Penalties;
using Data.Rewards;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    public class GiveItems: IChoice
    {
        [SerializeField, TextArea(1, 5)] string description;
        [SerializeField] ItemStatsSO requiredItem;
        [SerializeField] int targetQty;
        [SerializeField] int experience;
        [SerializeField, TextArea(1, 5)] string successMessage;
        [SerializeField, TextArea(1, 5)] string failureMessage;
        [SerializeField] List<IRewardStats> rewards = new();
        [SerializeField] List<IPenaltyStats> penalties = new();

        public IItemStats RequiredItem => requiredItem;
        public int TargetQty => targetQty;
        public string Description => description;
        public int Experience => experience;
        public string SuccessMessage => successMessage;
        public string FailureMessage => failureMessage;
        public List<IRewardStats> Rewards => rewards;
        public List<IPenaltyStats> Penalties => penalties;

        public bool Resolve(Party party)
        {
            if((int)party.CountItemInParty(requiredItem.Id) >= targetQty)
            {
                party.RemoveItemFromPartyInventory(requiredItem.Id, (double)targetQty);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Give {requiredItem.Id} x{targetQty}";
        }
    }
}
