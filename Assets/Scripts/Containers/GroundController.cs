using Data;
using Data.Items;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest
{
    public class GroundController : MonoBehaviour, IGroundController
    {
        IContainer _groundContainer;

        public IContainer GroundContainer => _groundContainer;

        List<IItem> deleteItems = new List<IItem>();

        public event EventHandler OnGroundCleared;


        private void Awake()
        {
            var groundStats = new ContainerStats(
                "ground",
                0.1f,
                0f,
                "The Ground",
                "",
                new Coor(8, 8));
            _groundContainer = (IContainer)ItemFactory.GetItem(groundStats);
            if (_groundContainer is not null)
                Debug.Log($"Ground created with size ({_groundContainer.Dimensions.row} x {_groundContainer.Dimensions.column})");
        }

        public void DestroyAllContainedObjects()
        {
            Debug.Log($"Destroying all items on ground", this);
            deleteItems.Clear();
            foreach (var content in _groundContainer.Contents.Values)
                deleteItems.Add(content.Item);
            for (int i = 0; i < deleteItems.Count; i++)
                _groundContainer.TryTake(out _, _groundContainer.Contents[deleteItems[i].GuId].GridSpaces[0]);
            OnGroundCleared?.Invoke(this, EventArgs.Empty);
        }
    }
}