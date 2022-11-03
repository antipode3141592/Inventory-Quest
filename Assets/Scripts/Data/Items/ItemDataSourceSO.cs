using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Data.Items
{
    public class ItemDataSourceSO : SerializedMonoBehaviour, IItemDataSource
    {
        [OdinSerialize] IItemStats _defaultItem;
        [OdinSerialize] Dictionary<string, IItemStats> _itemStats;

        public IItemStats GetById(string id)
        {
            if (_itemStats.ContainsKey(id))
                return _itemStats[id];
            return _defaultItem;
        }

        public IItemStats GetRandom()
        {
            return null;
        }

        public IItemStats GetItemByRarity(Rarity rarity)
        {
            var itemsOfRarity = _itemStats.Select(y => y).Where(x => x.Value.Rarity == rarity && x.Value.IsQuestItem == false);
            int randomIndex = Random.Range(0, itemsOfRarity.Count());
            return itemsOfRarity.ElementAt(randomIndex).Value;
        }
    }
}
