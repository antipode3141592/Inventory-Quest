using FiniteStateMachine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.QuestMachine;
using System;

namespace InventoryQuest.Managers
{
    public class GameOver : IState
    {
        IGameManager _gameManager;

        public GameOver(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            _gameManager.GameOver();
            DialogueManager.ResetDatabase(databaseResetOptions: DatabaseResetOptions.RevertToDefault);
            PixelCrushers.SaveSystem.ResetGameState();
            QuestMachine.GetQuestJournal().DestroyQuestInstances();
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
        }
    }
}
