using Data.Characters;
using Data.Penalties;
using Data.Rewards;
using System.Collections.Generic;

namespace Data.Encounters
{
    class EmptyChoice: IChoice
    {
        public EmptyChoice()
        {
        }

        public string Description => "";
        public string SuccessMessage => "";
        public string FailureMessage => "";
        public int Experience => 0;
        public List<IRewardStats> Rewards { get; } = new();
        public List<IPenaltyStats> Penalties { get; } = new();

        public bool Resolve(Party party) 
        {
            return true;
        }
    }
}
