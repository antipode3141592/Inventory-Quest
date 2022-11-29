using Data.Items;
using Data.Shapes;
using System;

namespace InventoryQuest.Managers
{
    public interface IGameManager
    {
        public event EventHandler OnGameBegining;

        public GameStates CurrentState { get; }
        public void ChangeState(GameStates targetState);
        public void BeginGame();
    }
}