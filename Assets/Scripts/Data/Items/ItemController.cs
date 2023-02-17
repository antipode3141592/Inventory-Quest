using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data.Items
{
    public class ItemController : MonoBehaviour, IItemController
    {

        public IItem CreateItem(IItemStats itemStats)
        {
            var item = ItemFactory.GetItem(itemStats);
            item.RequestDestruction += RequestDestroyHandler;
            return item;
        }

        

        public void RequestDestroyHandler(object sender, EventArgs e)
        {

            //if (sender is not IItem item) return;
            //if (!Contents.ContainsKey(item.GuId)) return;
            //var anchor = Contents[item.GuId].AnchorPosition;
            //TryTake(out _, anchor);
        }
    }
}
