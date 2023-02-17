using Data.Characters;
using Data.Penalties;
using Data.Rewards;
using System.Collections.Generic;

namespace Data.Encounters
{
    public class Ignore : IChoice
    {
        public string Description => "Ignore";
        public int Experience { get; }
        public string SuccessMessage { get; }
        public string FailureMessage { get; }
        public List<IRewardStats> Rewards { get; } = new();
        public List<IPenaltyStats> Penalties { get; } = new();
        public bool Resolve(Party party)
        {
            return true;
        }
    }
}
