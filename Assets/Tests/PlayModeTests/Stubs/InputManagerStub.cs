using Data.Items;
using Data.Shapes;
using InventoryQuest.Managers;
using System;
using UnityEngine;

namespace InventoryQuest.Testing
{
    public class InputManagerStub : MonoBehaviour, IInputManager
    {
        public IItem HoldingItem { get; set; }

        public event EventHandler OnItemHeld;
        public event EventHandler OnItemPlaced;
        public event EventHandler<RotationEventArgs> OnRotateCW;
        public event EventHandler<RotationEventArgs> OnRotateCCW;
        public event EventHandler OnSubmitDown;
        public event EventHandler OnSubmitHold;
        public event EventHandler OnSubmitUp;
        public event EventHandler CloseInventoryCommand;

        public void CloseInventory()
        {
            CloseInventoryCommand?.Invoke(this, EventArgs.Empty);
        }
    }
}
