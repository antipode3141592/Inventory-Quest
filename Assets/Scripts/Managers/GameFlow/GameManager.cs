using Data.Locations;
using FiniteStateMachine;
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
        IPartyManager _partyManager;

        [SerializeField] ILocationStats startingLocation;

        GameStates currentState;

        StateMachine _stateMachine;

        Initializing initializing;
        InMainMenu inMainMenu;
        InGame inGame;
        Paused paused;
        GameOver gameOver;
        Victory victory;

        public GameStates CurrentState { get { return currentState; } }

        public event EventHandler OnGameBegining;
        public event EventHandler OnGameOver;
        public event EventHandler OnGamePause;
        public event EventHandler OnGameWin;

        [Inject]
        public void Init(IAdventureManager adventureManager, IGameStateDataSource gameStateDataSource, IPartyManager partyManager)
        {
            _adventureManager = adventureManager;
            _gameStateDataSource = gameStateDataSource;
            _partyManager = partyManager;
        }

        void Awake()
        {
            currentState = GameStates.Loading;

            _stateMachine = new StateMachine();

            initializing = new Initializing();
            inMainMenu = new InMainMenu();
            inGame = new InGame();
            paused = new Paused();
            gameOver = new GameOver();
            victory = new Victory();

            At(initializing, inMainMenu, AreManagersLoaded());
            AtAny(gameOver, IsPartyDead());

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);


            Func<bool> AreManagersLoaded() => () => initializing.ManagersLoaded;
            Func<bool> IsPartyDead() => () => isPartyDead;
        }

        void Start()
        { 
            _adventureManager.Adventuring.StateEntered += OnAdventureStartedHandler;
            _adventureManager.Adventuring.StateExited += OnAdventureCompletedHandler;

            _partyManager.CurrentParty.OnPartyDeath += OnPartyDeathHandler;
        }

        bool isPartyDead = false;

        void OnPartyDeathHandler(object sender, EventArgs e)
        {
            Debug.Log($"OnPartyDeathHandler()", this);
            isPartyDead = true;
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

        public void GameBegin()
        {
            _gameStateDataSource.SetCurrentLocation(startingLocation.Id);
            OnGameBegining?.Invoke(this, EventArgs.Empty);
        }

        public void GameOver()
        {
            Debug.Log($"GameOver(), man!  GameOver()!", this);
            OnGameOver?.Invoke(this, EventArgs.Empty);
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

    public class Initializing : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool ManagersLoaded = false;

        public void OnEnter()
        {
            ManagersLoaded = false;
            BeginLoading();
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
        }

        void BeginLoading()
        {

        }
    }

    public class InMainMenu : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
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

    public class InGame : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
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

    public class Paused : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
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

    public class GameOver : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
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

    public class Victory : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
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
