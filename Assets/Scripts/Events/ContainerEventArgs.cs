using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventoryQuest;

namespace Data
{
    public class ContainerEventArgs: EventArgs
    {
        public Container Container;

        public ContainerEventArgs(Container container)
        {
            Container = container;
        }
    }
}
