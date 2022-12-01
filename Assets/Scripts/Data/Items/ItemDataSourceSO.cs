using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Data.Items
{
    public class ItemDataSourceSO : SerializedMonoBehaviour, IItemDataSource
    {
        [OdinSerialize] readonly IItemStats _defaultItem;
        [OdinSerialize] readonly List<IItemStats> _items;

        Dictionary<string, IItemStats> _itemStats;

        void Awake()
        {
            if (Debug.isDebugBuild)
                Debug.Log($"Initializing ItemDataSourceSO dictionary...");
            _itemStats = new();
            foreach(var item in _items)
            {
                _itemStats.Add(item.Id, item);
                if (Debug.isDebugBuild)
                    Debug.Log($"...adding {item.Id}");
            }
            if (Debug.isDebugBuild)
                Debug.Log($"Initialization of ItemDataSourceSO complete.");
        }

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
