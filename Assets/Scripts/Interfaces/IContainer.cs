using UnityEngine;

namespace Data
{
    public interface IContainer
    {
        public bool TryPlace(Item item, Vector2Int target);
        public bool TryTake(out Item item, Vector2Int target);
    }

    
}
