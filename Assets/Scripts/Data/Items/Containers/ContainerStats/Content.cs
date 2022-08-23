using System.Collections.Generic;

namespace Data.Items
{
    public class Content
    {
        public IItem Item;
        public List<Coor> GridSpaces;
        public Coor AnchorPosition;
        public int Quantity;

        public Content(IItem item, List<Coor> occupiedSpaces, Coor anchorPosition, int quantity = 1)
        {
            GridSpaces = occupiedSpaces;
            Item = item;
            AnchorPosition = anchorPosition;
            Quantity = quantity;
        }
    }


}
