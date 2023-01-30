namespace Data.Items
{
    public static class ItemPlacementHelpers
    {
        public static bool TryAutoPlaceToContainer(IContainer container, IItem item)
        {
            foreach(var point in container.Grid)
            {
                if (container.TryPlace(ref item, point.Key))
                    return true;
                if (!item.Shape.IsRotationallySymmetric)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        item.Rotate(1);
                        if (container.TryPlace(ref item, point.Key))
                            return true;
                    }
                }
            }
            return false;
        }
    }
}