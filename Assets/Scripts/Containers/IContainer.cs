using UnityEngine;

namespace Data
{
    public interface IContainer
    {
        public bool TryPlace(Item item, Coor target);
        public bool TryTake(out Item item, Coor target);
    }

    
}
