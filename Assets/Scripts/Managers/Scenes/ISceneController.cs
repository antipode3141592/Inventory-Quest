using System;

namespace InventoryQuest.Managers
{
    public interface ISceneController
    {
        public event EventHandler RequestShowLoading;
        public event EventHandler RequestHideLoading;
    }
}
