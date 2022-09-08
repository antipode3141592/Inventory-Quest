using Data.Items;
using System;

namespace InventoryQuest
{
    public interface IGroundController
    {
        public IContainer GroundContainer { get; }

        public event EventHandler OnGroundCleared;

        public void DestroyAllContainedObjects();
    }
}