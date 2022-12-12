using Data.Characters;
using Data.Penalties;
using Data.Rewards;
using System;
using System.Collections.Generic;

namespace Data.Encounters
{
    public class Ignore : IChoice
    {
        public string Description => throw new NotImplementedException();
        public int Experience { get; }
        public string SuccessMessage => throw new NotImplementedException();
        public string FailureMessage => throw new NotImplementedException();
        public List<IRewardStats> Rewards => throw new NotImplementedException();
        public List<IPenaltyStats> Penalties => throw new NotImplementedException();
        public bool Resolve(Party party)
        {
            return true;
        }
    }
}
