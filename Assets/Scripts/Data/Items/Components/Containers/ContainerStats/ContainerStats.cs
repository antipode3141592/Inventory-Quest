﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Items
{
    [Serializable]
    public class ContainerStats : IContainerStats
    {
        [SerializeField] bool[,] _initialGrid;

        ICollection<Coor> _grid;

        public ContainerStats(List<Coor> grid)
        {
            _grid = grid;
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
                                _grid.Add(new Coor(r, c));
                            }
                        }
                    }

                    
                }
                return _grid;
            }
        }
    }
}