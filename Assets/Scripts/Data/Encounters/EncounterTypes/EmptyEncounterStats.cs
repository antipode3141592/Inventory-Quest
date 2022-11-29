using Data.Penalties;
using Data.Rewards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Encounters
{
    public class EmptyEncounterStats : IEncounterStats

    {
        string id;
        string _name;
        string description;
        int experience;
        string successMessage;
        string failureMessage;
        List<IRewardStats> rewards = new();
        List<IPenaltyStats> penalties = new();

        public EmptyEncounterStats(string id)
        {
            this.id = id;
        }

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
