using Data;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest
{
    public class Content
    {
        public Item Item;
        public List<Vector2Int> GridSpaces;

        public Content(Item item, List<Vector2Int> occupiedSpaces)
        {
            GridSpaces = occupiedSpaces;
            Item = item;
        }
    }

    
}
