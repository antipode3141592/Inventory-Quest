﻿using Data;
using Data.Items;
using Data.Shapes;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Zenject;
using InventoryQuest.Managers;

namespace InventoryQuest.UI
{
    public class ContainerDisplay : MonoBehaviour
    {
        IGameManager _gameManager;
        IInputManager _inputManager;

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
        public void Init(IGameManager gameManager, IInputManager inputManager)
        {
            _gameManager = gameManager;
            _inputManager = inputManager;
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
                    square.Init(gameManager: _gameManager, inputManager: _inputManager);
                    square.transform.localPosition = new Vector2((float)c * squareWidth, -(float)r * squareWidth);
                    square.SetContainer(Container);
                    square.Coordinates = new Coor(r, c);
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
                squares[point.Key.row, point.Key.column].SetContainer(Container);
                squares[point.Key.row, point.Key.column].gameObject.SetActive(true);
                squares[point.Key.row, point.Key.column].IsOccupied = point.Value.IsOccupied;
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
                squares[point.Key.row, point.Key.column].IsOccupied = point.Value.IsOccupied;
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
    }
}
