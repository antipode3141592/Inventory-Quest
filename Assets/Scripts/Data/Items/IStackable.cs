using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Items
{
    public interface IStackable
    {
        public int Quantity { get; set; }
        public int MinStackSize { get;}
    }
}
