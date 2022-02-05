using Data;
using Inputs;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using Cinemachine;

namespace InventoryQuest.UI
{
    public class ContainerDisplay : MonoBehaviour
    {
        GameManager _gameManager;
        public Coor DisplaySize;
        public Vector2 Origin;

        [SerializeField]
        public GameObject GridSquareSprite; //squarre prefab

        SpriteRenderer[,] squares;

        Coor lastHoverOver = new Coor(0,0);

        MyControls _myControls;

        [SerializeField]
        ContactFilter2D _contactFilter;
        Physics2DRaycaster _physics2DRaycaster;
        [SerializeField]
        Camera _camera;
        

        private Container myContainer;
        public Container MyContainer 
        {
            get { return myContainer; } 
            set 
            { 
                myContainer = value;
                CreateGrid();
            }
        }

        public void Awake()
        {
            Origin = transform.position;

            //should be injected
            _gameManager = FindObjectOfType<GameManager>();

            _physics2DRaycaster = _camera.GetComponent<Physics2DRaycaster>();

            //input system
            _myControls = new MyControls();

            _myControls.Game.Enable();
            _myControls.Game.LeftClick.performed += OnPointerUp;
        }

        private void Update()
        {
            //hover over red/green highlighting only while holding a piece
            if (_gameManager.CurrentState == GameStates.HoldingItem)
            {

                
                Coor currentHoverOver = CursorToCoor(out Coor coor) ? coor : new Coor(-1,-1);
                //if (CursorToCoor(out Coor coor)) currentHoverOver = coor;
                if (currentHoverOver == lastHoverOver) return;
                int row = currentHoverOver.row;
                int column = currentHoverOver.column;

                SetSquareColor(lastHoverOver, GetGridSquareState(lastHoverOver));
                lastHoverOver = currentHoverOver;
                if (MyContainer.Grid[row, column].IsOccupied)
                {
                    SetSquareColor(currentHoverOver, GridSquareState.Incorrect);
                }
                else
                {
                    SetSquareColor(currentHoverOver, GridSquareState.Highlight);
                }
                //return;
                //SetSquareColor(lastHoverOver, GetGridSquareState(lastHoverOver));
                //lastHoverOver = new Coor(-1, -1);
            }
        }

        bool CursorToCoor(out Coor coordinates)
        {
            using (var pooledObject = ListPool<RaycastHit2D>.Get(out List<RaycastHit2D> hits))
            {
                Vector2 target = _myControls.Game.CursorPosition.ReadValue<Vector2>();
                Vector2 worldTarget = _camera.ScreenToWorldPoint(target);
                if (Physics2D.Raycast(worldTarget, -Vector2.up, _contactFilter, hits) >= 1)
                {
                    foreach (var hit in hits)
                    {
                        if (hit.transform.gameObject.TryGetComponent<ContainerGridSquareDisplay>(out ContainerGridSquareDisplay square))
                        {
                            coordinates = square.Coordinates;
                            return true;

                        }
                    }

                }
                coordinates = null;
                return false;
            }
        }

        GridSquareState GetGridSquareState(Coor coor)
        {
            if (MyContainer.IsPointInGrid(coor) 
                && MyContainer.Grid[coor.row, coor.column].IsOccupied) return GridSquareState.Occupied;
            return GridSquareState.Normal;
        }

        public void OnDestroy()
        {
            _myControls.Game.LeftClick.performed -= OnPointerUp;
        }

        public void CreateGrid()
        {
            squares = new SpriteRenderer[MyContainer.ContainerSize.row, MyContainer.ContainerSize.column];
            //draw squares
            for (int r = 0; r < MyContainer.ContainerSize.row; r++)
            {
                for (int c = 0; c < MyContainer.ContainerSize.column; c++)
                {
                    var square = Instantiate(original: GridSquareSprite, parent: transform);
                    square.transform.position = new Vector2(Origin.x + c, Origin.y - r);
                    var display = square.GetComponent<ContainerGridSquareDisplay>();
                    display.SetContainer(MyContainer);
                    display.Coordinates = new Coor(r, c);
                    squares[r, c] = square.GetComponent<SpriteRenderer>();
                }
            }
        }

        public void DestroyGrid()
        {
            foreach (var square in squares)
            {
                Destroy(square, 0.1f);
            }
        }

        public void OnContainerUpdate(object sender, GridEventArgs e)
        {
            foreach(var grid in e.GridPositions)
            {
                SetSquareColor(grid, e.State);
            }
        }

        public void SetSquareColor(Coor target, GridSquareState state)
        {
            if (!IsValidCoor(squares, target)) return;
            Color targetColor =
            state switch
            {
                GridSquareState.Occupied => Color.grey,
                GridSquareState.Highlight => Color.green,
                GridSquareState.Incorrect => Color.red,
                _ => Color.white
            };
            squares[target.row, target.column].color = targetColor;
        }

        bool IsValidCoor(SpriteRenderer[,] grid, Coor target) => target.row < grid.GetLength(0) && target.row >= 0 && target.column < grid.GetLength(1) && target.column >= 0;

        public void OnPointerUp(InputAction.CallbackContext context)
        {
            Coor target = CursorToCoor(out Coor coor) ? coor : new Coor(-1, -1);
            Debug.Log($"processing OnPointerUp() for {gameObject.name} at grid {target}");
            switch (_gameManager.CurrentState)
            {
                case GameStates.Default:
                    
                    if (MyContainer.TryTake(out var item, target))
                    {
                        _gameManager.HoldingItem = item;
                        _gameManager.ChangeState(GameStates.HoldingItem);
                    }
                    break;
                case GameStates.HoldingItem:
                    if (MyContainer.TryPlace(_gameManager.HoldingItem, target))
                    {
                        _gameManager.HoldingItem = null;
                        _gameManager.ChangeState(GameStates.Default);
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public class ContainerUpdateArgs : EventArgs {
        public Coor[] HighlightedSquares;
        public GridSquareState State;

        public ContainerUpdateArgs(Coor[] highlightedSquares, GridSquareState state)
        {
            HighlightedSquares = highlightedSquares;
            State = state;
        }
    }
}
