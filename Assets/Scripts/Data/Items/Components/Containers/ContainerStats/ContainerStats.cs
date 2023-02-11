using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Items
{
    public class ContainerStats : IContainerStats
    {
        [SerializeField, TableMatrix(Transpose = true)] bool[,] _initialGrid;
        [SerializeField] bool _canContainContainers;

        ICollection<Coor> _grid;

        public ContainerStats(List<Coor> grid, bool canContainContainers = false)
        {
            _grid = grid;
            _canContainContainers = canContainContainers;
        }

        public ContainerStats()
        {            
        }

        public ICollection<Coor> Grid {
            get {
                if (_grid is null)
                {
                    _grid = new List<Coor>();
                    if (_initialGrid is not null)
                    {
                        
                        for (int r = 0; r < _initialGrid.GetLength(0); r++)
                        {
                            for (int c = 0; c < _initialGrid.GetLength(1); c++)
                            {
                                if (_initialGrid[r,c])
                                    _grid.Add(new Coor(r, c));
                            }
                        }
                    }

                    
                }
                return _grid;
            }
        }

        public bool CanContainContainers => _canContainContainers;
    }
}
