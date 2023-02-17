using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Items
{
    public interface IItemController
    {
        public void RequestDestroyHandler(object sender, EventArgs e);
    }
}
