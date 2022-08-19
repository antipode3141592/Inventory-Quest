namespace Data.Items
{

    public static class ItemPlacementHelpers
    {

        public static bool TryAutoPlaceToContainer(ContainerBase container, IItem item)
        {
            for (int _r = 0; _r < container.Dimensions.row; _r++)
            {
                for (int _c = 0; _c < container.Dimensions.column; _c++)
                {
                    //try placing in default facing
                    if (container.TryPlace(item, new Coor(_r, _c)))
                        return true;
                    //try placing in each other facing
                    if (!item.Shape.IsRotationallySymmetric)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            item.Shape.Rotate(1);
                            if (container.TryPlace(item, new Coor(_r, _c)))
                                return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}