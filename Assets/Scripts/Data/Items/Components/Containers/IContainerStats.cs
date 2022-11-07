using System.Collections.Generic;

namespace Data.Items
{
    public interface IContainerStats: IItemComponentStats
    {
        public ICollection<Coor> Grid { get; }
    }
}
