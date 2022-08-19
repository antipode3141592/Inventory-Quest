using Data.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

namespace Data.Items
{
    public class Container : ContainerBase
    {
        
        

        public Container(ContainerStats stats) : base(stats)
        {
            Grid = new GridSquare[stats.ContainerSize.row, stats.ContainerSize.column];
            Dimensions = stats.ContainerSize;
        }
    }

}