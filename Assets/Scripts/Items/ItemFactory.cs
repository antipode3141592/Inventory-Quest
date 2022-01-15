using Data;
using InventoryQuest.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InventoryQuest
{
    public class ItemFactory
    {
        public static Item GetItem(ShapeType shape, ItemStats stats)
        {
            Shape _shape;
            switch (shape)
            {
                case ShapeType.Square1:
                    _shape = new Square1();
                    break;
                case ShapeType.Square2:
                    _shape = new Square2();
                    break;
                case ShapeType.T1:
                    _shape = new T1();
                    break;
                default:
                    Debug.Log($"item shape type not found, default single square");
                    _shape = new Square1();
                    break;
            }
            return new Item(itemStats: stats, itemShape: _shape);
        }
    }
}
