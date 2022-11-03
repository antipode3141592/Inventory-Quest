using System;

namespace Data.Items
{
    public class ContainerEventArgs : EventArgs
    {
        public IContainer Container;

        public ContainerEventArgs(IContainer container)
        {
            Container = container;
        }
    }
}
