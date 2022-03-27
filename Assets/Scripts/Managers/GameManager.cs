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
        RewardManager _rewardManager;

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

        GameStates currentState = GameStates.Loading;
        

        public GameStates CurrentState { get { return currentState; } }

        public EventHandler OnItemHeld;
        public EventHandler OnItemPlaced;
        public EventHandler<RotationEventArgs> OnRotateCW;
        public EventHandler<RotationEventArgs> OnRotateCCW;

        [Inject]
        public void Init(RewardManager rewardManager)
        {
            _rewardManager = rewardManager;
        }

        private void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);
        }

        private void Update()
        {
            switch (currentState)
            {
                case GameStates.Loading:
                    currentState = GameStates.Default;
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
