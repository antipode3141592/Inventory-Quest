using Data.Items;
using Data.Shapes;
using Rewired;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        IAdventureManager _adventureManager;

        Player player;
        int playerId = 0;

        private IItem holdingItem;
        public IItem HoldingItem
        {
            get => holdingItem;
            set
            {
                holdingItem = value;
                if (value is null) OnItemPlaced?.Invoke(this, EventArgs.Empty);
                else OnItemHeld?.Invoke(this, EventArgs.Empty);
            }
        }

        GameStates currentState;

        public GameStates CurrentState { get { return currentState; } }

        public event EventHandler OnItemHeld;
        public event EventHandler OnItemPlaced;
        public event EventHandler<RotationEventArgs> OnRotateCW;
        public event EventHandler<RotationEventArgs> OnRotateCCW;

        [Inject]
        public void Init(IAdventureManager adventureManager)
        {
            _adventureManager = adventureManager;
        }

        void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);
            currentState = GameStates.Loading;

            _adventureManager.Adventuring.StateEntered += OnAdventureStartedHandler;
            _adventureManager.Adventuring.StateExited += OnAdventureCompletedHandler;

            Debug.Log($"{Application.persistentDataPath}");
        }

        void OnAdventureCompletedHandler(object sender, EventArgs e)
        {
            _adventureManager.Idle.Continue();

        }

        void Update()
        {
            CheckRotateAction();
        }

        void OnAdventureStartedHandler(object sender, EventArgs e)
        {
            ChangeState(GameStates.Encounter);
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

            if (rotateCW)
            {
                var facing = HoldingItem.Shape.Rotate(1);
                Debug.Log($"CheckRotateAction() detected CW action");
                OnRotateCW?.Invoke(this, new RotationEventArgs(facing));
                return;
            }

            if (rotateCCW)
            {
                var facing = HoldingItem.Shape.Rotate(-1);
                Debug.Log($"CheckRotateAction() detected CCW action");
                OnRotateCCW?.Invoke(this, new RotationEventArgs(facing));
                return;
            }
        }
    }

    public enum GameStates { Loading, Encounter, ItemHolding }
}
