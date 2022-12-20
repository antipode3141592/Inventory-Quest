using Data.Items;
using Data.Shapes;
using System;

namespace InventoryQuest.Managers
{
    public interface IGameManager
    {
        public event EventHandler OnGameBegining;
        public event EventHandler OnGameOver;
        public event EventHandler OnGamePause;
        public event EventHandler OnGameWin;

        public GameStates CurrentState { get; }
        public void ChangeState(GameStates targetState);
        public void GameBegin();

        public void GameOver();

        public void GameWin();

        public void GamePause();

        public void MainMenuOpen();
    }
}