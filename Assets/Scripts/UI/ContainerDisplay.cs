using Data;
using Data.Items;
using Data.Shapes;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace InventoryQuest.UI
{
    public class ContainerDisplay : MonoBehaviour
    {
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

        IContainer myContainer;
        public IContainer MyContainer 
        {
            get { return myContainer; } 
            set 
            {
                DestroyGrid();
                RemoveAllItemSprites();
                myContainer = value;
                SetupGrid();
                SetItemSprites();
            }
        }

        void Awake()
        {
            InitializeDisplay();
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
                    square.SetContainer(MyContainer);
                    square.Coordinates = new Coor(r, c);
                    squares[r, c] = square;
                    squares[r, c].gameObject.SetActive(false);
                }
            }
        }

        public void SetupGrid()
        {
            if (MyContainer is null) return;
            foreach(var point in MyContainer.Grid) 
            { 
                squares[point.Key.row, point.Key.column].SetContainer(MyContainer);
                squares[point.Key.row, point.Key.column].gameObject.SetActive(true);
                squares[point.Key.row, point.Key.column].IsOccupied = point.Value.IsOccupied;
            }

            MyContainer.OnItemPlaced += OnItemChangeHandler;
            MyContainer.OnItemTaken += OnItemChangeHandler;
            MyContainer.OnMatchingItems += MatchedItems;
        }

        void MatchedItems(object sender, HashSet<string> e)
        {
            foreach (var itemGuid in e)
            {
                foreach (var coor in MyContainer.Contents[itemGuid].GridSpaces)
                    squares[coor.row, coor.column].SetHighlightColor(HighlightState.Match, 2f);
            }
        }

        public void DestroyGrid()
        {
            foreach (var square in squares)
            {
                square.IsOccupied = false;
                square.SetContainer(null);
                square.gameObject.SetActive(false);
            }

            if (MyContainer is null) return;
            MyContainer.OnItemPlaced -= OnItemChangeHandler;
            MyContainer.OnItemTaken -= OnItemChangeHandler;
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
            foreach (var point in MyContainer.Grid)
            {
                squares[point.Key.row, point.Key.column].IsOccupied = point.Value.IsOccupied;
            }
        }

        public void SetItemSprites()
        {
            if (MyContainer is null) return;
            foreach(var content in MyContainer.Contents)
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
    }
}
