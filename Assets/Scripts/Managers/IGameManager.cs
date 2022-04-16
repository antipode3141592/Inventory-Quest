using Data.Items;
using Data.Shapes;
using System;

namespace InventoryQuest.Managers
{
    public interface IGameManager
    {
        GameStates CurrentState { get; }
        IItem HoldingItem { get; set; }

        void ChangeState(GameStates targetState);
        void CheckRotateAction();

        public event EventHandler OnItemHeld;
        public event EventHandler OnItemPlaced;
        public event EventHandler<RotationEventArgs> OnRotateCW;
        public event EventHandler<RotationEventArgs> OnRotateCCW;
    }
}