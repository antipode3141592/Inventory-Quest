using Data;
using Data.Items;
using Data.Items.Components;
using Data.Shapes;
using InventoryQuest.Managers;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace InventoryQuest.UI
{
    public class ContainerDisplay : MonoBehaviour
    {
        IGameManager _gameManager;
        IInputManager _inputManager;
        IPartyManager _partyManager;
        IEncounterManager _encounterManager;

        [SerializeField] Transform _panelTransform;
        [SerializeField] Transform _itemPanelTransform;
        [SerializeField] ItemImage itemImagePrefab;
        [SerializeField] ContainerGridSquareDisplay gridSquarePrefab; //square prefab

        ContainerGridSquareDisplay[,] squares;
        List<ItemImage> itemImages;

        [SerializeField] int rowMax;
        [SerializeField] int columnMax;

        float squareWidth;

        [SerializeField] ContactFilter2D _contactFilter;

        public ContainerGridSquareDisplay[,] Squares => squares;
        public List<ItemImage> ItemImages => itemImages;

        IContainer _container;
        public IContainer Container => _container;

        [Inject]
        public void Init(IGameManager gameManager, IInputManager inputManager, IPartyManager partyManager, IEncounterManager encounterManager)
        {
            _gameManager = gameManager;
            _inputManager = inputManager;
            _partyManager = partyManager;
            _encounterManager = encounterManager;
        }

        void Awake()
        {
            InitializeDisplay();
        }

        void Start()
        {
            _inputManager.OnRotateCCW += ItemRotationHandler;
            _inputManager.OnRotateCW += ItemRotationHandler;
        }

        

        #region Grid Creation and Destruction
        [Button]
        public void InitializeDisplay()
        {
            squares = new ContainerGridSquareDisplay[rowMax, columnMax];
            itemImages = new List<ItemImage>();
            squareWidth = gridSquarePrefab.Width;
            InitializeGrid();
        }

        public void InitializeGrid()
        {
            for (int r = 0; r < rowMax; r++)
            {
                for(int c = 0; c < columnMax; c++)
                {
                    ContainerGridSquareDisplay square = Instantiate(original: gridSquarePrefab, parent: _panelTransform);
                    square.transform.localPosition = new Vector2((float)c * squareWidth, -(float)r * squareWidth);
                    square.Coordinates = new Coor(r, c);
                    square.GridSquarePointerClicked += GridSquareClicked;
                    square.GridSquarePointerEntered += GridSquareEntered;
                    square.GridSquarePointerExited += GridSquareExited;
                    squares[r, c] = square;
                    squares[r, c].gameObject.SetActive(false);
                }
            }
        }

        public void SetContainer(IContainer container)
        {
            DestroyGrid();
            RemoveAllItemSprites();
            _container = container;
            SetupGrid();
            SetItemSprites();
        }

        void SetupGrid()
        {
            if (Container is null)
            {
                if (Debug.isDebugBuild)
                    Debug.LogWarning($"MyContainer is null for this ContainerDisplay", this);
                return; 
            }
            foreach(var point in Container.Grid) 
            { 
                Squares[point.Key.row, point.Key.column].gameObject.SetActive(true);
                Squares[point.Key.row, point.Key.column].IsOccupied = point.Value.IsOccupied;
            }

            Container.OnItemPlaced += OnItemChangeHandler;
            Container.OnItemTaken += OnItemChangeHandler;
            Container.OnMatchingItems += MatchedItems;
        }

        void MatchedItems(object sender, HashSet<string> e)
        {
            foreach (var itemGuid in e)
            {
                foreach (var coor in Container.Contents[itemGuid].GridSpaces)
                    Squares[coor.row, coor.column].SetHighlightColor(HighlightState.Match, 2f);
            }
        }

        public void DestroyGrid()
        {
            foreach (var square in Squares)
            {
                square.IsOccupied = false;
                square.gameObject.SetActive(false);
            }

            if (Container is null) return;
            Container.OnItemPlaced -= OnItemChangeHandler;
            Container.OnItemTaken -= OnItemChangeHandler;
        }

        public void OnItemChangeHandler(object sender, string e)
        {
            UpdateGridState();
            RemoveAllItemSprites();
            SetItemSprites();
        }
        #endregion

        public void UpdateGridState()
        {
            foreach (var point in Container.Grid)
                Squares[point.Key.row, point.Key.column].IsOccupied = point.Value.IsOccupied;
        }

        public void SetItemSprites()
        {
            if (Container is null) return;
            foreach(var content in Container.Contents)
            {
                IItem item = content.Value.Item;
                Facing facing = item.CurrentFacing;
                Coor AnchorPosition = content.Value.AnchorPosition;
                Sprite sprite = item.Sprite;
                ItemImage itemImage = Instantiate<ItemImage>(original: itemImagePrefab, parent: _itemPanelTransform);
                int quantity = item.Quantity;
                itemImage.SetItem(item.GuId, sprite, quantity);
                ImageUtilities.RotateSprite(facing, itemImage.Image, squares[AnchorPosition.row, AnchorPosition.column].transform.localPosition);
                itemImages.Add(itemImage);
            }
        }

        public void RemoveAllItemSprites()
        {
            if (itemImages is null || itemImages.Count == 0) return;
            for (int i = 0; i < itemImages.Count; i++)
            {
                Destroy(itemImages[i].gameObject);
            }
            itemImages.Clear();
        }

        public int GetContainerGridCount()
        {
            int runningTotal = 0;
            foreach(var square in squares)
            {
                if (!square.gameObject.activeInHierarchy)
                    continue;
                runningTotal++;
            }
            return runningTotal;
        }

        void GridSquareEntered(object sender, PointerEventData e)
        {
            HighlightGrid((sender as ContainerGridSquareDisplay).Coordinates);
        }

        void HighlightGrid(Coor anchorPoint)
        {
            List<Tuple<HighlightState, Coor>> tempPointList = UnityEngine.Pool.ListPool<Tuple<HighlightState, Coor>>.Get();
            if (_gameManager.CurrentState != GameStates.ItemHolding) return;
            _container.GetPointHighlights(ref tempPointList, _inputManager.HoldingItem, anchorPoint);
            if (tempPointList.Count == 0) return;
            for (int i = 0; i < tempPointList.Count; i++)
                Squares[tempPointList[i].Item2.row, tempPointList[i].Item2.column].SetHighlightColor(tempPointList[i].Item1);
            UnityEngine.Pool.ListPool<Tuple<HighlightState, Coor>>.Release(tempPointList);
        }

        void GridSquareExited(object sender, PointerEventData e)
        {
            ResetGrid();
        }

        void ResetGrid()
        {
            foreach (var square in Squares)
                square.SetHighlightColor(HighlightState.Normal);
        }

        void GridSquareClicked(object sender, PointerEventData e)
        {
            var clickedCoor = (sender as ContainerGridSquareDisplay).Coordinates;
            if (e.button == PointerEventData.InputButton.Left)
            {
                LeftClickRepsonse(clickedCoor);
            }
            else if (e.button == PointerEventData.InputButton.Right)
            {
                RightClickResponse(clickedCoor);
            }
        }

        void RightClickResponse(Coor clickedCoor)
        {
            var itemGuid = _container.Grid[clickedCoor].storedItemGuId;
            if (_container.Contents[itemGuid].Item.Components.ContainsKey(typeof(IUsable)))
            {
                var _usable = (_container.Contents[itemGuid].Item.Components[typeof(IUsable)] as IUsable);
                var _character = _partyManager.CurrentParty.Characters[_partyManager.CurrentParty.SelectedPartyMemberGuId];
                _usable.TryUse(_character);
                if (_usable is EncounterLengthEffect encounterEffect)
                    _encounterManager.AddEncounterModifier(new EncounterModifier(_character, encounterEffect.EncounterLengthEffectStats.Modifiers));
            }
        }

        void LeftClickRepsonse(Coor clickedCoor)
        {
            switch (_gameManager.CurrentState)
            {
                case GameStates.Encounter:
                    if (_container.TryTake(out var item, clickedCoor))
                    {
                        _inputManager.HoldingItem = item;
                        _gameManager.ChangeState(GameStates.ItemHolding);
                    }
                    break;
                case GameStates.ItemHolding:
                    if (_container.TryPlace(_inputManager.HoldingItem, clickedCoor))
                    {
                        _inputManager.HoldingItem = null;
                        _gameManager.ChangeState(GameStates.Encounter);
                    }
                    break;
                default:
                    break;
            }
        }

        void ItemRotationHandler(object sender, RotationEventArgs e)
        {
            ResetGrid();
            foreach(var square in Squares)
                if (square.IsPointerHovering)
                {
                    HighlightGrid(square.Coordinates);
                    break;
                }
        }
    }
}
