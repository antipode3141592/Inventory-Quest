using Data;
using Rewired;
using Rewired.Integration.UnityUI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace InventoryQuest.UI
{
    public class ContainerDisplay : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        GameManager _gameManager;
        public Coor DisplaySize;
        public Vector2 Origin;

        [SerializeField]
        public ContainerGridSquareDisplay GridSquareSprite; //squarre prefab

        ContainerGridSquareDisplay[,] squares;

        Coor lastHoverOver = new Coor(-1,-1);

        [SerializeField]
        ContactFilter2D _contactFilter;
        [SerializeField]
        Camera _camera;

        Player player;
        PlayerMouse mouse;
        int playerId = 0;


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

            player = ReInput.players.GetPlayer(playerId);
            mouse = PlayerMouse.Factory.Create();
            mouse.playerId = playerId;

            //default to center of screen
            mouse.screenPosition = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
        }

        private void Update()
        {
            //hover over red/green highlighting only while holding a piece
            if (_gameManager.CurrentState == GameStates.HoldingItem)
            {
                Coor currentHoverOver = CursorToCoor(out Coor coor) ? coor : new Coor(-1,-1);

                if (currentHoverOver == lastHoverOver) return;
                int row = currentHoverOver.row;
                int column = currentHoverOver.column;

                SetSquareColor(lastHoverOver, GetGridSquareState(lastHoverOver));
                lastHoverOver = currentHoverOver;
                if (MyContainer.IsPointInGrid(currentHoverOver) && MyContainer.Grid[row, column].IsOccupied)
                {
                    SetSquareColor(currentHoverOver, GridSquareState.Incorrect);
                }
                else
                {
                    SetSquareColor(currentHoverOver, GridSquareState.Highlight);
                }
            }
        }

        bool CursorToCoor(out Coor coordinates)
        {
            using (var pooledObject = ListPool<RaycastHit2D>.Get(out List<RaycastHit2D> hits))
            {
                Vector2 target = mouse.screenPosition;
                Vector2 worldTarget = _camera.ScreenToWorldPoint(target);
                Debug.Log($"target: {target} , worldTarget: {worldTarget}", gameObject);
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

        public void CreateGrid()
        {
            squares = new ContainerGridSquareDisplay[MyContainer.ContainerSize.row, MyContainer.ContainerSize.column];
            //draw squares
            for (int r = 0; r < MyContainer.ContainerSize.row; r++)
            {
                for (int c = 0; c < MyContainer.ContainerSize.column; c++)
                {
                    ContainerGridSquareDisplay square = Instantiate(original: GridSquareSprite, parent: transform);
                    square.transform.position = new Vector2(Origin.x + c, Origin.y - r);
                    square.SetContainer(MyContainer);
                    square.Coordinates = new Coor(r, c);
                    squares[r, c] = square;
                }
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
            if (!myContainer.IsPointInGrid(target)) return;
            squares[target.row, target.column].SetColor(state);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"OnPointerDown()", gameObject);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PlayerPointerEventData playerEventData = (PlayerPointerEventData)eventData;
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
}
