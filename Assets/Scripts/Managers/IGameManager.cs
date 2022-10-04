using Data.Items;
using Data.Shapes;
using System;

namespace InventoryQuest.Managers
{
    public interface IGameManager
    {
        public event EventHandler OnItemHeld;
        public event EventHandler OnItemPlaced;
        public event EventHandler<RotationEventArgs> OnRotateCW;
        public event EventHandler<RotationEventArgs> OnRotateCCW;
        public event EventHandler OnGameBegining;

        public GameStates CurrentState { get; }
        public IItem HoldingItem { get; set; }
        public void ChangeState(GameStates targetState);
        public void CheckRotateAction();
        public void BeginGame();
    }
}