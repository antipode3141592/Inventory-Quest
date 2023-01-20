using Data.Locations;
using FiniteStateMachine;
using InventoryQuest.Audio;
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
        IAudioManager _audioManager;

        [SerializeField] ILocationStats startingLocation;
        [SerializeField] GameMusicSettings gameMusicSettings;

        StateMachine _stateMachine;

        Initializing initializing;
        InMainMenu inMainMenu;
        InGame inGame;
        Paused paused;
        GameOver gameOver;
        Victory victory;

        bool returnToMainMenu = false;
        bool gaming = false;

        public bool IsGameOver { get; set; }
        public bool IsGameBegining { get; set; }

        public event EventHandler OnGameBegining;
        public event EventHandler OnGameOver;
        public event EventHandler OnGamePause;
        public event EventHandler OnGameWin;
        public event EventHandler OnGameRestart;

        [Inject]
        public void Init(IAdventureManager adventureManager, IGameStateDataSource gameStateDataSource, IPartyManager partyManager, IAudioManager audioManager)
        {
            _adventureManager = adventureManager;
            _gameStateDataSource = gameStateDataSource;
            _partyManager = partyManager;
            _audioManager = audioManager;
        }

        void Awake()
        {
            _stateMachine = new StateMachine(this);

            initializing = new Initializing();
            inMainMenu = new InMainMenu(_audioManager);
            inGame = new InGame();
            paused = new Paused();
            gameOver = new GameOver(this, _audioManager, gameMusicSettings);
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
        }

        void Update()
        {
            _stateMachine.Tick();
        }

        public void GameBegin()
        {
            _gameStateDataSource.SetDestinationLocation(startingLocation.Id);
            _partyManager.IsPartyDead = false;
            returnToMainMenu = false;
            gaming = true;
            IsGameBegining = true;
            OnGameBegining?.Invoke(this, EventArgs.Empty);
        }

        public void GameOver()
        {
            gaming = false;
            Debug.Log($"-------------------------------------");
            Debug.Log($"GameOver(), man!  GameOver()!", this);
            Debug.Log($"-------------------------------------");
            IsGameOver = true;
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
