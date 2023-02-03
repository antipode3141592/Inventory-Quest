using Data.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class ContainerManager: MonoBehaviour, IContainerManager
    {
        IItemDataSource _itemDataSource;

        readonly List<IItem> deleteItems = new();

        public IDictionary<string, IContainer> Containers { get; protected set; } = new Dictionary<string, IContainer>();

        public event EventHandler OnContainersAvailable;
        public event EventHandler OnContainersDestroyed;
        public event EventHandler<IContainer> OnContainerSelected;

        [Inject]
        public void Init(IItemDataSource itemDataSource)
        {
            _itemDataSource = itemDataSource;
        }

        public IContainer AddContainer(string itemId)
        {
            var lootPile = ItemFactory.GetItem(_itemDataSource.GetById(itemId));
            var lootContainer = lootPile.Components[typeof(IContainer)] as IContainer;
            if (lootContainer is null) return null;
            Containers.Add(lootContainer.GuId, lootContainer);
            OnContainersAvailable?.Invoke(this, EventArgs.Empty);
            return lootContainer;
        }
        public void DestroyContainers()
        {
            Debug.Log($"Destroying reward piles and items", this);
            deleteItems.Clear();
            foreach (var container in Containers.Values)
            {
                foreach (var content in container.Contents.Values)
                {
                    deleteItems.Add(content.Item);
                }
                deleteItems.Add(container.Item);
            }

            for (int i = 0; i < deleteItems.Count; i++)
            {
                deleteItems[i] = null;
            }
            Containers.Clear();
            OnContainersDestroyed?.Invoke(this, EventArgs.Empty);
        }
        public void SelectContainer(string containerGuid)
        {
            if (Containers.ContainsKey(containerGuid))
                OnContainerSelected?.Invoke(this, Containers[containerGuid]);
        }
    }
}