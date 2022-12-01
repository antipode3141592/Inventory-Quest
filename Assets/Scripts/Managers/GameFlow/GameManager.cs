using Data.Items;
using Data.Shapes;
using Rewired;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        IAdventureManager _adventureManager;

        GameStates currentState;

        public GameStates CurrentState { get { return currentState; } }

        public event EventHandler OnGameBegining;

        [Inject]
        public void Init(IAdventureManager adventureManager)
        {
            _adventureManager = adventureManager;
        }

        void Awake()
        {
            currentState = GameStates.Loading;

            _adventureManager.Adventuring.StateEntered += OnAdventureStartedHandler;
            _adventureManager.Adventuring.StateExited += OnAdventureCompletedHandler;
        }

        void OnAdventureCompletedHandler(object sender, EventArgs e)
        {
            _adventureManager.Idle.Continue();
        }

        void OnAdventureStartedHandler(object sender, EventArgs e)
        {
            ChangeState(GameStates.Encounter);
        }

        public void ChangeState(GameStates targetState)
        {
            if (currentState == targetState) return;
            currentState = targetState;
        }

        public void BeginGame()
        {
            OnGameBegining?.Invoke(this, EventArgs.Empty);
        }
    }
}
