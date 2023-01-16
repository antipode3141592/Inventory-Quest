using Data.Locations;
using FiniteStateMachine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.QuestMachine;
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

        StateMachine _stateMachine;

        Initializing initializing;
        InMainMenu inMainMenu;
        InGame inGame;
        Paused paused;
        GameOver gameOver;
        Victory victory;

        bool returnToMainMenu = false;
        bool gaming = false;

        public event EventHandler OnGameBegining;
        public event EventHandler OnGameOver;
        public event EventHandler OnGamePause;
        public event EventHandler OnGameWin;
        public event EventHandler OnGameRestart;

        [Inject]
        public void Init(IAdventureManager adventureManager, IGameStateDataSource gameStateDataSource, IPartyManager partyManager)
        {
            _adventureManager = adventureManager;
            _gameStateDataSource = gameStateDataSource;
            _partyManager = partyManager;
        }

        void Awake()
        {
            _stateMachine = new StateMachine();

            initializing = new Initializing();
            inMainMenu = new InMainMenu();
            inGame = new InGame();
            paused = new Paused();
            gameOver = new GameOver(this);
            victory = new Victory(this);

            At(initializing, inMainMenu, AreManagersLoaded());
            At(inMainMenu, inGame, IsGaming());
            AtAny(inMainMenu, GoToMainMenu());
            AtAny(gameOver, IsPartyDead());

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);

            Func<bool> AreManagersLoaded() => () => initializing.ManagersLoaded;
            Func<bool> IsGaming() => () => gaming;
            Func<bool> GoToMainMenu() => () => returnToMainMenu;
            Func<bool> IsPartyDead() => () => _partyManager.IsPartyDead;
        }

        void Start()
        {
            _stateMachine.SetState(initializing);
            _adventureManager.Adventuring.StateExited += OnAdventureCompletedHandler;
        }

        void Update()
        {
            _stateMachine.Tick();
        }

        void OnAdventureCompletedHandler(object sender, EventArgs e)
        {
            _adventureManager.Idle.Continue();
        }

        public void GameBegin()
        {
            _gameStateDataSource.SetCurrentLocation(startingLocation.Id);
            _partyManager.IsPartyDead = false;
            returnToMainMenu = false;
            gaming = true;
            OnGameBegining?.Invoke(this, EventArgs.Empty);
        }

        public void GameOver()
        {
            gaming = false;
            Debug.Log($"-------------------------------------");
            Debug.Log($"GameOver(), man!  GameOver()!", this);
            Debug.Log($"-------------------------------------");
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
            returnToMainMenu = true;
            OnGameRestart?.Invoke(this, EventArgs.Empty);
        }
    }
}
