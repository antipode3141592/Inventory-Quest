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
            for (int r = 0; r < e.Container.ContainerSize.row; r++)
            {
                for (int c = 0; c < e.Container.ContainerSize.column; c++)
                {

                }
            }
        }
    }
}
