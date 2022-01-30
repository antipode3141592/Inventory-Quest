using Data;
using System;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class ContainerDisplay : MonoBehaviour
    {
        public Coor DisplaySize;
        public Vector2 Origin;

        public GameObject GridSquareSprite; //squarre prefab

        GameObject[,] squares;

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
            
        }

        public void OnDestroy()
        {

        }

        public void CreateGrid()
        {
            squares = new GameObject[MyContainer.ContainerSize.row, MyContainer.ContainerSize.column];
            //draw squares
            for (int r = 0; r < MyContainer.ContainerSize.row; r++)
            {
                for (int c = 0; c < MyContainer.ContainerSize.column; c++)
                {
                    GameObject square = Instantiate(GridSquareSprite);
                    square.transform.position = new Vector2(Origin.x + c, Origin.y - r);
                    squares[r, c] = square;
                }
            }
        }

        public void DestroyGrid()
        {
            foreach (GameObject square in squares)
            {
                Destroy(square, 0.1f);
            }
        }

        public void OnContainerUpdate(object sender, ContainerUpdateArgs e)
        {

        }

        public void OnContainerClose(object sender, EventArgs e)
        {

        }

        public void SetSquareColor(GridSquareState state)
        {
            
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

    

    public enum GridSquareState { Normal, Highlight, Incorrect, Occupied }
}
