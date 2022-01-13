using Data;
using System.Collections.Generic;

namespace InventoryQuest
{
    public class Content
    {
        public Item Item;
        public List<Coor> GridSpaces;

        public Content(Item item, List<Coor> occupiedSpaces)
        {
            GridSpaces = occupiedSpaces;
            Item = item;
        }
    }

    
}
