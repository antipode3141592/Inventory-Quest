using FiniteStateMachine;
using InventoryQuest.Audio;
using PixelCrushers.DialogueSystem;
using PixelCrushers.QuestMachine;
using System;

namespace InventoryQuest.Managers
{
    public class GameOver : IState
    {
        readonly IGameManager _gameManager;
        readonly IAudioManager _audioManager;
        readonly GameMusicSettings _gameMusicSettings;

        public GameOver(IGameManager gameManager, IAudioManager audioManager, GameMusicSettings gameMusicSettings)
        {
            _gameManager = gameManager;
            _audioManager = audioManager;
            _gameMusicSettings = gameMusicSettings;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            _gameManager.GameOver();
            _audioManager.PlayMusicTrack(_gameMusicSettings.GameOverMusic);
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
