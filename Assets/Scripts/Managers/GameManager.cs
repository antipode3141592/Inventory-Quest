using Data;
using Data.Items;
using Data.Shapes;
using Rewired;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class GameManager : MonoBehaviour
    {
        RewardManager _rewardManager;
        EncounterManager _encounterManager;
        AdventureManager _adventureManager;

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
        public void Init(RewardManager rewardManager, EncounterManager encounterManager, AdventureManager adventureManager)
        {
            _rewardManager = rewardManager;
            _encounterManager = encounterManager;
            _adventureManager = adventureManager;
        }

        private void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);
            currentState = GameStates.Loading;
            //test stuff
            _rewardManager.EnqueueReward("spirit_ring");
            _rewardManager.EnqueueReward("power_sword");
            _rewardManager.EnqueueReward("uncommon_loot_pile_gigantic");

            _adventureManager.Loading();
            
        }

        private void Update()
        {
            CheckRotateAction();
        }

        private void OnRewardsProcessCompleteHandler(object sender, EventArgs e)
        {
            ChangeState(GameStates.EncounterPreparing);
        }

        public void ChangeState(GameStates targetState)
        {
            if (currentState == targetState) return;
            currentState = targetState;
        }
        public void CheckRotateAction()
        {
            if (HoldingItem is null) return;
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

    public enum GameStates { Loading, EncounterPreparing, ItemHolding, EncounterResolving }
}
