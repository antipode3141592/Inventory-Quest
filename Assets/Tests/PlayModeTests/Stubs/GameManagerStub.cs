using Data.Items;
using Data.Shapes;
using InventoryQuest.Managers;
using System;
using UnityEngine;


namespace InventoryQuest.Testing
{
    public class GameManagerStub : MonoBehaviour, IGameManager
    {

        public bool rotatePieceCW = false;
        public bool rotatePieceCCW = false;

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

        public bool IsGameOver { get; set; }
        public bool IsGameBegining { get; set; }

        public event EventHandler OnItemHeld;
        public event EventHandler OnItemPlaced;
        public event EventHandler<RotationEventArgs> OnRotateCW;
        public event EventHandler<RotationEventArgs> OnRotateCCW;
        public event EventHandler OnGameBegining;
        public event EventHandler OnSubmitDown;
        public event EventHandler OnSubmitHold;
        public event EventHandler OnSubmitUp;
        public event EventHandler OnGameOver;
        public event EventHandler OnGamePause;
        public event EventHandler OnGameWin;
        public event EventHandler OnGameRestart;

        void Awake()
        {
            currentState = GameStates.Encounter;
        }

        public void ChangeState(GameStates targetState)
        {
            if (currentState == targetState) return;
            currentState = targetState;
        }

        public void GameBegin()
        {
            OnGameBegining?.Invoke(this, EventArgs.Empty);
        }

        public void GameOver()
        {
            throw new NotImplementedException();
        }

        public void GameWin()
        {
            throw new NotImplementedException();
        }

        public void GamePause()
        {
            throw new NotImplementedException();
        }

        public void MainMenuOpen()
        {
            throw new NotImplementedException();
        }
    }
}
