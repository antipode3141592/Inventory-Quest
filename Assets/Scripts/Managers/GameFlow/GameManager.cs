using Data.Locations;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class GameManager : SerializedMonoBehaviour, IGameManager
    {
        IAdventureManager _adventureManager;
        IGameStateDataSource _gameStateDataSource;

        [SerializeField] ILocationStats startingLocation;

        GameStates currentState;

        public GameStates CurrentState { get { return currentState; } }

        public event EventHandler OnGameBegining;

        [Inject]
        public void Init(IAdventureManager adventureManager, IGameStateDataSource gameStateDataSource)
        {
            _adventureManager = adventureManager;
            _gameStateDataSource = gameStateDataSource;
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
            _gameStateDataSource.SetCurrentLocation(startingLocation.Id);
            OnGameBegining?.Invoke(this, EventArgs.Empty);
        }
    }
}
