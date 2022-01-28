using System;

namespace InventoryQuest
{
    public class ContainerEventArgs : EventArgs
    {
        public Container Container;

        public ContainerEventArgs(Container container)
        {
            Container = container;
        }
    }
}
