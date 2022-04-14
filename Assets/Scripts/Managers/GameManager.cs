using Data;
using Data.Items;
using Data.Shapes;
using Rewired;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class GameManager : MonoBehaviour
    {
        RewardManager _rewardManager;
        EncounterManager _encounterManager;
        AdventureManager _adventureManager;

        Player player;
        int playerId = 0;

        [SerializeField] float startDelay = 1f;

        private IItem holdingItem;
        public IItem HoldingItem 
        { 
            get => holdingItem; 
            set {
                holdingItem = value; 
                if (value is null) OnItemPlaced?.Invoke(this, EventArgs.Empty);
                else OnItemHeld?.Invoke(this, EventArgs.Empty);
            }
        }

        GameStates currentState;

        public GameStates CurrentState { get { return currentState; } }

        public EventHandler OnItemHeld;
        public EventHandler OnItemPlaced;
        public EventHandler<RotationEventArgs> OnRotateCW;
        public EventHandler<RotationEventArgs> OnRotateCCW;

        [Inject]
        public void Init(RewardManager rewardManager, EncounterManager encounterManager, AdventureManager adventureManager)
        {
            _rewardManager = rewardManager;
            _encounterManager = encounterManager;
            _adventureManager = adventureManager;
        }

        private void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);
            currentState = GameStates.Loading;

            //testing stuff

            _adventureManager.OnAdventureStarted += OnAdventureStartedHandler;
            _adventureManager.OnAdventureCompleted += OnAdventureCompletedHandler;
        }

        private void OnAdventureCompletedHandler(object sender, EventArgs e)
        {
            Debug.Log($"OnAdventureCompleted handled by {gameObject.name}", this);
        }

        private void Start()
        {
            StartCoroutine(Pathfind("intro_path"));
        }

        IEnumerator Pathfind(string id)
        {
            Debug.Log($"Staring delay...", this);
            yield return new WaitForSeconds(startDelay);
            _adventureManager.ChoosePath(id);
            Debug.Log($"Pathfinding!", this);
        }

        private void Update()
        {
            CheckRotateAction();
        }

        private void OnAdventureStartedHandler(object sender, EventArgs e)
        {
            ChangeState(GameStates.Encounter);
        }

        public void ChangeState(GameStates targetState)
        {
            if (currentState == targetState) return;
            currentState = targetState;
        }
        public void CheckRotateAction()
        {
            if (HoldingItem is null) return;
            bool rotateCW = player.GetButtonUp("RotatePieceCW");
            bool rotateCCW = player.GetButtonUp("RotatePieceCCW");

            if (rotateCW) { 
                var facing = HoldingItem.Shape.Rotate(1); 
                Debug.Log($"CheckRotateAction() detected CW action");
                OnRotateCW?.Invoke(this, new RotationEventArgs(facing));
                return;
            }

            if (rotateCCW) {
                var facing = HoldingItem.Shape.Rotate(-1);
                Debug.Log($"CheckRotateAction() detected CCW action");
                OnRotateCCW?.Invoke(this, new RotationEventArgs(facing));
                return;
            }
        }
    }

    public enum GameStates { Loading, Encounter, ItemHolding }
}
