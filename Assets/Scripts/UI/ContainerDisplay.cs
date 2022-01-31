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

        SpriteRenderer[,] squares;

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
            squares = new SpriteRenderer[MyContainer.ContainerSize.row, MyContainer.ContainerSize.column];
            //draw squares
            for (int r = 0; r < MyContainer.ContainerSize.row; r++)
            {
                for (int c = 0; c < MyContainer.ContainerSize.column; c++)
                {
                    var square = Instantiate(GridSquareSprite);
                    square.transform.position = new Vector2(Origin.x + c, Origin.y - r);
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
