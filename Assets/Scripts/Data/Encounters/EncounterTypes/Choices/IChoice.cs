using Data.Characters;
using Data.Penalties;
using Data.Rewards;
using System.Collections.Generic;

namespace Data.Encounters
{
    public interface IChoice
    {
        public string Description { get; }
        public string SuccessMessage { get; }
        public string FailureMessage { get; }
        public int Experience { get; }
        public List<IRewardStats> Rewards { get; }
        public List<IPenaltyStats> Penalties { get; }

        public bool Resolve(Party party);
    }
}