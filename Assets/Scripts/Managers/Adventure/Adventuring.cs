using FiniteStateMachine;
using InventoryQuest.Audio;
using System;
using UnityEngine;

namespace InventoryQuest.Managers.States
{
    public class Adventuring : IState
    {
        readonly IEncounterManager _encounterManager;
        readonly IGameStateDataSource _gameStateDataSource;
        readonly IAudioManager _audioManager;
        
        public bool EndAdventure = false;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public Adventuring(IEncounterManager encounterManager, IGameStateDataSource gameStateDataSource, IAudioManager audioManager)
        {
            _encounterManager = encounterManager;
            _gameStateDataSource = gameStateDataSource;
            _audioManager = audioManager;
        }

        public void OnEnter()
        {
            EndAdventure = false;
            StartAdventure();
            _encounterManager.Idle.StateEntered += EncounterIdle;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        void EncounterIdle(object sender, EventArgs e)
        {
            EndAdventure = true;
        }

        public void OnExit()
        {
            _encounterManager.Idle.StateEntered -= EncounterIdle;
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        public void StartAdventure()
        {
            Debug.Log($"StartAdventure()...");
            _gameStateDataSource.SetCurrentPath();
            if (_gameStateDataSource.CurrentPathStats == null) return;
            _audioManager.PlayMusicTrack(_gameStateDataSource.CurrentPathStats.AudioClip);
            _encounterManager.Idle.Continue();
        }
    }
}