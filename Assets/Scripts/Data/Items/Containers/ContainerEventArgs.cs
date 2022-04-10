using System;

namespace Data.Items
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
