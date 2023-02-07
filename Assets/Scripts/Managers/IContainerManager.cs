using Data.Items;
using System;
using System.Collections.Generic;

namespace InventoryQuest.Managers
{
    public interface IContainerManager
    {
        public IDictionary<string, IContainer> Containers { get; }

        public event EventHandler OnContainersAvailable;
        public event EventHandler OnContainersDestroyed;
        public event EventHandler<IContainer> OnContainerSelected;

        public IContainer AddNewContainer(string itemId);
        public void AddContainer(IContainer container);
        public void DestroyContainers();
        public void SelectContainer(string containerGuid);
    }
}