using FiniteStateMachine;
using InventoryQuest.Audio;
using System;

namespace InventoryQuest.Managers
{
    public class InMainMenu : IState
    {
        readonly IAudioManager _audioManager;

        public InMainMenu(IAudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            _audioManager.StopMusicTrack();
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
