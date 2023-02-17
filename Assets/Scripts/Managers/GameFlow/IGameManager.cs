using System;

namespace InventoryQuest.Managers
{
    public interface IGameManager
    {
        public bool IsGameOver { get; set; }
        public bool IsGameBegining { get; set; }

        public event EventHandler OnGameBeginning;
        public event EventHandler OnGameOver;
        public event EventHandler OnGamePause;
        public event EventHandler OnGameWin;
        public event EventHandler OnGameRestart;

        public void GameBegin();
        public void GameOver();
        public void GameWin();
        public void GamePause();
        public void MainMenuOpen();
    }
}