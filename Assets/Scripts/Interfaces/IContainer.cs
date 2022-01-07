using UnityEngine;

namespace Data
{
    public interface IContainer
    {
        public bool TryPlace(Item item, Vector2Int target);
        public Item Take(Item item);
    }

    
}
