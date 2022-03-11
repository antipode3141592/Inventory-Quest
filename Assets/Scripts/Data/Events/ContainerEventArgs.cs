using System;

namespace Data
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
