using Data.Interfaces;
using Data.Rewards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class RewardManager: MonoBehaviour
    {
        PartyManager _partyManager;
        IRewardDataSource _rewardDataSource;



        public void ProcessReward(string rewardId)
        {
            //get IRewardStats from IRewardDataSource

            //get IReward from RewardFactory


        }
    }
}
