using Data.Items;
using Data.Shapes;
using System;


namespace InventoryQuest.Managers
{
    public interface IInputManager
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

        public void CloseInventory();
    }
}
