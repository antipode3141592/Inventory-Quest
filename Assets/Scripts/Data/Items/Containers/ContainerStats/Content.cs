using System.Collections.Generic;

namespace Data.Items
{
    public class Content
    {
        public IItem Item;
        public List<Coor> GridSpaces;
        public Coor AnchorPosition;

        public Content(IItem item, List<Coor> occupiedSpaces, Coor anchorPosition)
        {
            GridSpaces = occupiedSpaces;
            Item = item;
            AnchorPosition = anchorPosition;
        }
    }


}
