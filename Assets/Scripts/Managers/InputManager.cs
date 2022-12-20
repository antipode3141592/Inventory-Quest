using Data;
using Data.Characters;
using Data.Items;
using Data.Items.Components;
using Data.Shapes;
using FiniteStateMachine;
using Rewired;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace InventoryQuest.Managers
{
    public class InputManager : MonoBehaviour, IInputManager
    {
        IPartyManager _partyManager;
        IEncounterManager _encounterManager;

        Player player;
        readonly int playerId = 0;

        StateMachine _stateMachine;

        HoldingItem _holdingItem;
        Normal _normal;

        IItem holdingItem;
        public IItem HoldingItem
        {
            get => holdingItem;
            set
            {
                holdingItem = value;
                if (value is null) OnItemPlaced?.Invoke(this, EventArgs.Empty);
                else OnItemHeld?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler OnItemHeld;
        public event EventHandler OnItemPlaced;

        public event EventHandler OnSubmitDown;
        public event EventHandler OnSubmitHold;
        public event EventHandler OnSubmitUp;
        public event EventHandler<RotationEventArgs> OnRotateCW;
        public event EventHandler<RotationEventArgs> OnRotateCCW;

        public event EventHandler OpenInventoryCommand;
        public event EventHandler CloseInventoryCommand;

        [Inject]
        public void Init(IPartyManager partyManager, IEncounterManager encounterManager)
        {
            _partyManager = partyManager;
            _encounterManager = encounterManager;
        }

        void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);

            _stateMachine = new StateMachine();

            _holdingItem = new HoldingItem(this);
            _normal = new Normal(this);

            At(_normal, _holdingItem, ItemPickedUp());
            At(_holdingItem, _normal, ItemReleased());

            Func<bool> ItemReleased() => () => holdingItem is null;
            Func<bool> ItemPickedUp() => () => holdingItem is not null;

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            //void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);
        }

        void Start()
        {
            if (Debug.isDebugBuild)
                Debug.Log($"InputManager started with rewired input {player.id}", this);
            _stateMachine.SetState(_normal);
        }

        void Update()
        {
            _stateMachine.Tick();
        }

        public void CheckRotateAction()
        {
            if (HoldingItem is null) return;
            bool rotateCW = player.GetButtonUp("RotatePieceCW");
            bool rotateCCW = player.GetButtonUp("RotatePieceCCW");

            if (rotateCW)
            {
                HoldingItem.Rotate(1);
                OnRotateCW?.Invoke(this, new RotationEventArgs(HoldingItem.CurrentFacing));
            }

            if (rotateCCW)
            {
                HoldingItem.Rotate(-1);
                Debug.Log($"CheckRotateAction() detected CCW action");
                OnRotateCCW?.Invoke(this, new RotationEventArgs(HoldingItem.CurrentFacing));
            }
        }

        public void CheckSubmitAction()
        {
            if (player.GetButtonDown("UISubmit"))
                OnSubmitDown?.Invoke(this, EventArgs.Empty);
            if (player.GetButton("UISubmit"))
                OnSubmitHold?.Invoke(this, EventArgs.Empty);
            if (player.GetButtonUp("UISubmit"))
                OnSubmitUp?.Invoke(this, EventArgs.Empty);
        }

        public void OpenInventory()
        {
            OpenInventoryCommand?.Invoke(this, EventArgs.Empty);
        }

        public void CloseInventory()
        {
            CloseInventoryCommand?.Invoke(this, EventArgs.Empty);
        }

        public void ContainerDisplayClickHandler(IContainer container, PointerEventData pointerEventData, Coor clickedCoor)
        {
            if (pointerEventData.button == PointerEventData.InputButton.Left)
            {
                LeftClickRepsonse(container, clickedCoor);
            }
            else if (pointerEventData.button == PointerEventData.InputButton.Right)
            {
                RightClickResponse(container, clickedCoor);
            }
        }

        public bool EquipmentSlotPointerClickHandler(PointerEventData eventData, ICharacter character, string slotId)
        {
            if (HoldingItem is null) {
                if (character.EquipmentSlots[slotId].TryUnequip(out var currentEquipment))
                {
                    if (currentEquipment is null) return false;
                    HoldingItem = currentEquipment as IItem;
                    return true;
                }
            } else { 
                if (character.EquipmentSlots[slotId].TryEquip(out var previousItem, HoldingItem))
                {
                    HoldingItem = previousItem as IItem;
                }
            }
            return false;
        }

        void RightClickResponse(IContainer container, Coor clickedCoor)
        {
            var itemGuid = container.Grid[clickedCoor].storedItemGuId;
            if (container.Contents.ContainsKey(itemGuid) && container.Contents[itemGuid].Item.Components.ContainsKey(typeof(IUsable)))
            {
                var _usable = (container.Contents[itemGuid].Item.Components[typeof(IUsable)] as IUsable);
                var character = _partyManager.CurrentParty.Characters[_partyManager.CurrentParty.SelectedPartyMemberGuId];
                _usable.TryUse(character);
                if (_usable is EncounterLengthEffect encounterEffect)
                    _encounterManager.AddEncounterModifier(new EncounterModifier(character, encounterEffect.EncounterLengthEffectStats.Modifiers));
            }
        }

        void LeftClickRepsonse(IContainer container, Coor clickedCoor)
        {
            if (HoldingItem is null)
            {
                Debug.Log($"TryTake()", this);
                if (container.TryTake(out var item, clickedCoor))
                    HoldingItem = item;
            }
            else
            {
                Debug.Log($"TryPlace()", this);
                if (container.TryPlace(HoldingItem, clickedCoor))
                    HoldingItem = null;
            }
        }
    }

    public class Normal : IState 
    {
        IInputManager _inputManager;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public Normal(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

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
            _inputManager.CheckSubmitAction();
        }
    }


    public class HoldingItem: IState
    {
        IInputManager _inputManager;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public HoldingItem(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

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
            _inputManager.CheckRotateAction();
            _inputManager.CheckSubmitAction();
        }
    }
}
