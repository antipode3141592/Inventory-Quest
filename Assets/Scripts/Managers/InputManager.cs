using Data.Items;
using Data.Shapes;
using Rewired;
using System;
using UnityEngine;

namespace InventoryQuest.Managers
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        Player player;
        readonly int playerId = 0;

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

        public event EventHandler OnItemHeld;
        public event EventHandler OnItemPlaced;

        public event EventHandler OnSubmitDown;
        public event EventHandler OnSubmitHold;
        public event EventHandler OnSubmitUp;
        public event EventHandler<RotationEventArgs> OnRotateCW;
        public event EventHandler<RotationEventArgs> OnRotateCCW;

        public event EventHandler OpenInventoryCommand;
        public event EventHandler CloseInventoryCommand;

        void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);
        }

        void Start()
        {
            if (Debug.isDebugBuild)
                Debug.Log($"InputManager started with rewired input {player.id}", this);
        }

        void Update()
        {
            CheckRotateAction();
            CheckSubmitAction();
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
            }

            if (rotateCCW)
            {
                HoldingItem.Rotate(-1);
                Debug.Log($"CheckRotateAction() detected CCW action");
                OnRotateCCW?.Invoke(this, new RotationEventArgs(HoldingItem.CurrentFacing));
            }
        }

        void CheckSubmitAction()
        {
            if (player.GetButtonDown("UISubmit"))
                OnSubmitDown?.Invoke(this, EventArgs.Empty);
            if (player.GetButton("UISubmit"))
                OnSubmitHold?.Invoke(this, EventArgs.Empty);
            if (player.GetButtonUp("UISubmit"))
                OnSubmitUp?.Invoke(this, EventArgs.Empty);
        }

        public void OpenInventory()
        {
            OpenInventoryCommand?.Invoke(this, EventArgs.Empty);
        }

        public void CloseInventory()
        {
            CloseInventoryCommand?.Invoke(this, EventArgs.Empty);
        }
    }
}