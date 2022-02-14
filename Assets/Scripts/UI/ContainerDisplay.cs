using Data;
using UnityEngine;
using UnityEngine.UI;
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
        ContactFilter2D _contactFilter;


        private Container myContainer;
        public Container MyContainer 
        {
            get { return myContainer; } 
            set 
            {
                if (value is null) DestroyGrid();
                myContainer = value;
                CreateGrid();
            }
        }

        [Inject]
        public void Init(GameManager gameManager, ContainerDisplayManager containerDisplayManager)
        {
            _gameManager = gameManager;
            _containerDisplayManager = containerDisplayManager;

        }

        #region Grid Creation and Destruction

        public void CreateGrid()
        {
            if (MyContainer is null) return;
            float size = GridSquareSprite.Width;
            squares = new ContainerGridSquareDisplay[MyContainer.ContainerSize.row, MyContainer.ContainerSize.column];
            //draw squares
            for (int r = 0; r < MyContainer.ContainerSize.row; r++)
            {
                for (int c = 0; c < MyContainer.ContainerSize.column; c++)
                {
                    ContainerGridSquareDisplay square = Instantiate(original: GridSquareSprite, parent: _parent);
                    square.transform.localPosition = new Vector2((float)c * size, -(float)r * size);
                    square.SetContainer(MyContainer);
                    square.Coordinates = new Coor(r, c);
                    squares[r, c] = square;
                }
            }

            MyContainer.OnGridOccupied += OnGridSpacesOccupied;
            MyContainer.OnGridUnoccupied += OnGridSpacesUnoccupied;
        }

        public void DestroyGrid()
        {
            MyContainer.OnGridOccupied -= OnGridSpacesOccupied;
            MyContainer.OnGridUnoccupied -= OnGridSpacesUnoccupied;

            foreach (var square in squares)
            {

            }
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
