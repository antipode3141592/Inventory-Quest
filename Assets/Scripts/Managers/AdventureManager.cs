using Data.Encounters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

using Zenject;

namespace InventoryQuest.Managers
{
    public class AdventureManager: MonoBehaviour
    {
        EncounterManager _encounterManager;
        RewardManager _rewardManager;

        IEncounterDataSource _dataSource;

        int currentIndex;

        public IPath CurrentPath { get; set; }
        public ILocation StartLocation { get; set; }
        public ILocation EndLocation { get; set; }

        public EventHandler OnEncounterListGenerated;
        public EventHandler OnAdventureStarted;
        public EventHandler OnAdventureComplete;

        [Inject]
        public void Init(IEncounterDataSource dataSource, EncounterManager encounterManager, RewardManager rewardManager)
        {
            _dataSource = dataSource;
            _encounterManager = encounterManager;
            _rewardManager = rewardManager;
        }

        private void Awake()
        {
            _encounterManager.OnEncounterComplete += OnEncounterCompleteHandler;

            _rewardManager.OnRewardsProcessComplete += OnRewardsProcessCompleteHandler;
        }

        private void OnEncounterCompleteHandler(object sender, EventArgs e)
        {
            _rewardManager.DestroyRewards();
            Loading();
        }

        private void OnRewardsProcessCompleteHandler(object sender, EventArgs e)
        {
            
        }

        public void Loading()
        {
            _rewardManager.ProcessRewards();
        }

        public void ChoosePath()
        {
            //set current path 


            OnEncounterListGenerated?.Invoke(this, EventArgs.Empty);
            currentIndex = 0;
            LoadEncounter(CurrentPath.EncounterIds[currentIndex]);
            
        }

        public void LoadEncounter(string id)
        {
            if(id == string.Empty)
                _encounterManager.CurrentEncounter = EncounterFactory.GetEncounter(_dataSource.GetRandomEncounter());
            _encounterManager.CurrentEncounter = EncounterFactory.GetEncounter(_dataSource.GetEncounterById(id));
        }
    }

    public enum AdventureStates { Idle, Pathfinding, Encountering }
    /* Idle - in town, menu, or whatever
 * Pathfinding - pick endpoint location on map, load path (list of encounters) data
 * Encountering - iterate through list of encounters
 * 
 */

    public interface IPathStats
    {

    }

    public interface ILocationStats
    {

    }

    public interface IPathDataSource
    {
        public IPathStats GetPathById(string id);
    }

}



