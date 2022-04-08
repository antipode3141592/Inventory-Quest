using Data;
using Data.Interfaces;
using Rewired;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class GameManager : MonoBehaviour
    {
        const string encounterId = "test_of_might";

        RewardManager _rewardManager;
        EncounterManager _encounterManager;

        Player player;
        int playerId = 0;

        private IItem holdingItem;
        public IItem HoldingItem 
        { 
            get => holdingItem; 
            set {
                holdingItem = value; 
                if (value is null) OnItemPlaced?.Invoke(this, EventArgs.Empty);
                else OnItemHeld?.Invoke(this, EventArgs.Empty);
            }
        }

        GameStates currentState;
        

        public GameStates CurrentState { get { return currentState; } }

        public EventHandler OnItemHeld;
        public EventHandler OnItemPlaced;
        public EventHandler<RotationEventArgs> OnRotateCW;
        public EventHandler<RotationEventArgs> OnRotateCCW;

        [Inject]
        public void Init(RewardManager rewardManager, EncounterManager encounterManager)
        {
            _rewardManager = rewardManager;
            _encounterManager = encounterManager;
        }

        private void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);
            currentState = GameStates.Loading;
            _rewardManager.OnRewardsProcessComplete += OnRewardsProcessCompleteHandler;
            _encounterManager.OnEncounterComplete += OnEncounterCompleteHandler;
        }

        private void OnEncounterCompleteHandler(object sender, EventArgs e)
        {
            _rewardManager.DestroyRewards();
            Loading();
        }

        private void Start()
        {
            _rewardManager.EnqueueReward("spirit_ring");
            _rewardManager.EnqueueReward("power_sword");
            _rewardManager.EnqueueReward("uncommon_loot_pile_gigantic");
            Loading();
        }

        private void Update()
        {
            switch (currentState)
            {
                case GameStates.Loading:
                    
                    break;
                case GameStates.Default:
                    break;
                case GameStates.HoldingItem:
                    CheckRotateAction();
                    break;
                default:
                    break;
            }
        }

        private void OnRewardsProcessCompleteHandler(object sender, EventArgs e)
        {
            ChangeState(GameStates.Default);
        }

        void Loading()
        {
            _encounterManager.BeginAdventure();
            
            _rewardManager.ProcessRewards();
            
        }

        public void ChangeState(GameStates targetState)
        {
            if (currentState == targetState) return;
            currentState = targetState;
        }
        public void CheckRotateAction()
        {

            bool rotateCW = player.GetButtonUp("RotatePieceCW");
            bool rotateCCW = player.GetButtonUp("RotatePieceCCW");

            if (rotateCW) { 
                var facing = HoldingItem.Shape.Rotate(1); 
                Debug.Log($"CheckRotateAction() detected CW action");
                OnRotateCW?.Invoke(this, new RotationEventArgs(facing));
                return;
            }

            if (rotateCCW) {
                var facing = HoldingItem.Shape.Rotate(-1);
                Debug.Log($"CheckRotateAction() detected CCW action");
                OnRotateCCW?.Invoke(this, new RotationEventArgs(facing));
                return;
            }
        }
    }

    public enum GameStates { Loading, Default, HoldingItem}
}
