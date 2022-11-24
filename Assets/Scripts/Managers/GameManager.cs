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

        IItem holdingItem;
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
        public event EventHandler OnGameBegining;
        public event EventHandler OnSubmitDown;
        public event EventHandler OnSubmitUp;

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

        
        }

        void OnAdventureCompletedHandler(object sender, EventArgs e)
        {
            _adventureManager.Idle.Continue();

        }

        void Update()
        {
            CheckRotateAction();
            CheckSubmitAction();
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

        void CheckRotateAction()
        {
            if (HoldingItem is null) return;
            bool rotateCW = player.GetButtonUp("RotatePieceCW");
            bool rotateCCW = player.GetButtonUp("RotatePieceCCW");

            if (rotateCW)
            {
                HoldingItem.Rotate(1);
                OnRotateCW?.Invoke(this, new RotationEventArgs(HoldingItem.CurrentFacing));
                return;
            }

            if (rotateCCW)
            {
                HoldingItem.Rotate(-1);
                Debug.Log($"CheckRotateAction() detected CCW action");
                OnRotateCCW?.Invoke(this, new RotationEventArgs(HoldingItem.CurrentFacing));
                return;
            }
        }

        void CheckSubmitAction()
        {
            if (player.GetButtonDown("UISubmit"))
                OnSubmitDown?.Invoke(this, EventArgs.Empty);
            if (player.GetButtonUp("UISubmit"))
                OnSubmitUp?.Invoke(this, EventArgs.Empty);
        }



        public void BeginGame()
        {
            OnGameBegining?.Invoke(this, EventArgs.Empty);
        }
    }
}
