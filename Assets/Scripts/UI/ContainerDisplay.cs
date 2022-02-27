using Data;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class ContainerDisplay : MonoBehaviour
    {
        GameManager _gameManager;
        [SerializeField]
        Transform _parent;
        ContainerDisplayManager _containerDisplayManager;
        public Coor DisplaySize;
        public Vector2 Origin;

        [SerializeField]
        public ContainerGridSquareDisplay GridSquareSprite; //square prefab

        ContainerGridSquareDisplay[,] squares;
        [SerializeField]
        int rowMax;
        [SerializeField]
        int columnMax;

        float squareWidth;

        [SerializeField]
        ContactFilter2D _contactFilter;


        private Container myContainer;
        public Container MyContainer 
        {
            get { return myContainer; } 
            set 
            {
                DestroyGrid();
                myContainer = value;
                SetupGrid();
            }
        }

        [Inject]
        public void Init(GameManager gameManager, ContainerDisplayManager containerDisplayManager)
        {
            _gameManager = gameManager;
            _containerDisplayManager = containerDisplayManager;

        }

        private void Awake()
        {
            squares = new ContainerGridSquareDisplay[rowMax, columnMax];
            squareWidth = GridSquareSprite.Width;
            InitializeGrid();
        }

        private void Start()
        {
            
        }


        #region Grid Creation and Destruction

        public void InitializeGrid()
        {
            for (int r = 0; r < rowMax; r++)
            {
                for(int c = 0; c < columnMax; c++)
                {
                    ContainerGridSquareDisplay square = Instantiate(original: GridSquareSprite, parent: _parent);
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
            for (int r = 0; r < MyContainer.ContainerSize.row; r++)
            {
                for (int c = 0; c < MyContainer.ContainerSize.column; c++)
                {
                    squares[r, c].SetContainer(MyContainer);
                    squares[r, c].gameObject.SetActive(true);
                    squares[r, c].IsOccupied = MyContainer.Grid[r, c].IsOccupied;

                }
            }

            MyContainer.OnGridOccupied += OnGridSpacesOccupied;
            MyContainer.OnGridUnoccupied += OnGridSpacesUnoccupied;
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
            MyContainer.OnGridOccupied -= OnGridSpacesOccupied;
            MyContainer.OnGridUnoccupied -= OnGridSpacesUnoccupied;
        }

        public void OnGridSpacesOccupied(object sender, GridEventArgs e)
        {
            foreach(var coor in e.GridPositions)
            {
                squares[coor.row, coor.column].IsOccupied = true;
            }
        }

        public void OnGridSpacesUnoccupied(object sender, GridEventArgs e)
        {
            foreach(var coor in e.GridPositions)
            {
                squares[coor.row, coor.column].IsOccupied = false;
            }
        }
        #endregion

    }
}
