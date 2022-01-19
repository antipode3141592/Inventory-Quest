using System.Collections.Generic;

namespace Data
{
    public class Content
    {
        public IItem Item;
        public List<Coor> GridSpaces;

        public Content(IItem item, List<Coor> occupiedSpaces)
        {
            GridSpaces = occupiedSpaces;
            Item = item;
        }
    }


}
