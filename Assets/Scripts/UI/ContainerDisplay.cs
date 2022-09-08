using Data;
using Data.Items;
using Data.Shapes;
using System.Collections.Generic;
using UnityEngine;

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
            squares = new ContainerGridSquareDisplay[rowMax, columnMax];
            itemImages = new List<ItemImage>();
            squareWidth = gridSquarePrefab.Width;
            InitializeGrid();
        }

        #region Grid Creation and Destruction

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
            for (int r = 0; r < MyContainer.Dimensions.row; r++)
            {
                for (int c = 0; c < MyContainer.Dimensions.column; c++)
                {
                    squares[r, c].SetContainer(MyContainer);
                    squares[r, c].gameObject.SetActive(true);
                    squares[r, c].IsOccupied = MyContainer.Grid[r, c].IsOccupied;
                }
            }

            MyContainer.OnItemPlaced += OnItemChangeHandler;
            MyContainer.OnItemTaken += OnItemChangeHandler;
            MyContainer.OnMatchingItems += MatchedItems;
            //MyContainer.OnStackComplete += ItemStackComplete;
        }

        //void ItemStackComplete(object sender, HashSet<string> e)
        //{
        //    Debug.Log($"Stack of {e.Count} items complete!");
        //    foreach (var item in e)
        //    {
        //        Debug.Log($"{item}");
        //    }
        //}

        void MatchedItems(object sender, HashSet<string> e)
        {
            //Debug.Log($"There are {e.Count} matching adjacent items");
            foreach (var itemGuid in e)
            {
                //Debug.Log($"{itemGuid}");
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

        public void OnItemChangeHandler(object sender, GridEventArgs e)
        {
            UpdateGridState();
            RemoveAllItemSprites();
            SetItemSprites();
        }

        #endregion


        public void UpdateGridState()
        {
            for (int r = 0; r < MyContainer.Dimensions.row; r++)
            {
                for (int c = 0; c < MyContainer.Dimensions.column; c++)
                {
                    squares[r, c].IsOccupied = MyContainer.Grid[r, c].IsOccupied;
                }
            }
        }

        public void SetItemSprites()
        {
            if (MyContainer is null) return;
            foreach(var content in MyContainer.Contents)
            {
                IItem item = content.Value.Item;
                Facing facing = item.Shape.CurrentFacing;
                Coor AnchorPosition = content.Value.AnchorPosition;
                Sprite sprite = item.Sprite;
                ItemImage itemImage = Instantiate<ItemImage>(original: itemImagePrefab, parent: _itemPanelTransform);
                int quantity = item.Quantity;
                itemImage.SetItem(sprite, quantity);
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
    }
}
