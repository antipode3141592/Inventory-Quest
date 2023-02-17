using Data;
using Data.Items;
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
        IInputManager _inputManager;

        [SerializeField] Transform _panelTransform;
        [SerializeField] Transform _itemPanelTransform;
        [SerializeField] ItemImage itemImagePrefab;
        [SerializeField] ContainerGridSquareDisplay gridSquarePrefab; //square prefab

        ContainerGridSquareDisplay[,] squares;
        List<ItemImage> itemImages;

        [SerializeField] int rowMax;
        [SerializeField] int columnMax;

        public int RowMax => rowMax;
        public int ColumnMax => columnMax;

        float squareWidth;

        [SerializeField] ContactFilter2D _contactFilter;

        public ContainerGridSquareDisplay[,] Squares => squares;
        public List<ItemImage> ItemImages => itemImages;

        IContainer _container;
        public IContainer Container => _container;

        [Inject]
        public void Init(IInputManager inputManager)
        {
            _inputManager = inputManager;
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

        [Button]
        public void DestroyGridDisplay()
        {

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
                Squares[point.Key.row, point.Key.column].Show();
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
                square.Hide();
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
                Vector3 localPosition = squares[AnchorPosition.row, AnchorPosition.column].transform.localPosition;
                itemImage.SetItem(item.GuId, sprite, quantity, localPosition);
                ImageUtilities.RotateSprite(facing, itemImage.Image, localPosition);
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
                if (!square.IsActive)
                    continue;
                runningTotal++;
            }
            return runningTotal;
        }

        void GridSquareEntered(object sender, PointerEventData e)
        {
            var coor = (sender as ContainerGridSquareDisplay).Coordinates;
            HighlightGrid(coor);
            ViewDetailsTimerStart(coor);
        }

        void HighlightGrid(Coor anchorPoint)
        {
            if (_inputManager.HoldingItem is null || _container is null) return;

            List<Tuple<HighlightState, Coor>> tempPointList = UnityEngine.Pool.ListPool<Tuple<HighlightState, Coor>>.Get();
            _container.GetPointHighlights(ref tempPointList, _inputManager.HoldingItem, anchorPoint);
            for (int i = 0; i < tempPointList.Count; i++)
                Squares[tempPointList[i].Item2.row, tempPointList[i].Item2.column].SetHighlightColor(tempPointList[i].Item1);
            UnityEngine.Pool.ListPool<Tuple<HighlightState, Coor>>.Release(tempPointList);
        }

        void GridSquareExited(object sender, PointerEventData e)
        {
            ResetGrid();
            ViewDetailsTimerReset();
        }

        void ResetGrid()
        {
            foreach (var square in Squares)
                square.SetHighlightColor(HighlightState.Normal);
        }

        void GridSquareClicked(object sender, PointerEventData e)
        {
            var clickedCoor = (sender as ContainerGridSquareDisplay).Coordinates;
            _inputManager.ContainerDisplayClickHandler(_container, e, clickedCoor);
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

        void ViewDetailsTimerStart(Coor coor)
        {
            if (_container is null) return;
            if (!_container.Grid.ContainsKey(coor)) return;
            if (!_container.Grid[coor].IsOccupied) return;
            _inputManager.ShowItemDetails(_container.Contents[_container.Grid[coor].storedItemGuId].Item);
        }

        void ViewDetailsTimerReset()
        {
            _inputManager.HideItemDetails();
        }
    }
}
