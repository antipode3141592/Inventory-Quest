using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using UnityEngine;

namespace InventoryQuest.UI
{
    public class ContainerDisplay : MonoBehaviour
    {
        
        public void Awake()
        {
            
        }

        public void OnContainerUpdate(object sender, ContainerEventArgs e)
        {
            for (int i = 0; i < e.Container.Size.x; i++)
            {
                for (int j = 0; j < e.Container.Size.y; j++)
                {

                }
            }
        }
    }
}
