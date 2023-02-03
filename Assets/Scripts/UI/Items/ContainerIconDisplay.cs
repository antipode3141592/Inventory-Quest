using InventoryQuest.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class ContainerIconDisplay : MonoBehaviour, IOnMenuShow, IContainersDisplay
    {
        IContainerManager _containerManager;

        readonly List<ContainerIcon> lootIcons = new();

        [SerializeField]
        ContainerIcon _lootIconPrefab;

        [Inject]
        public void Init(IContainerManager containerManager)
        {
            _containerManager = containerManager;
        }

        void Start()
        {
            _containerManager.OnContainersAvailable += OnContainersAvailableHandler;
            _containerManager.OnContainersDestroyed += OnContainersDestroyedHandler;
        }

        void OnContainersAvailableHandler(object sender, EventArgs e)
        {
            SetContainerIcons();
        }

        void OnContainersDestroyedHandler(object sender, EventArgs e)
        {
            DestroyContainers();
        }

        public void ContainerSelected(string containerGuid)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"ContainerSelected({containerGuid})...");
            foreach (var icon in lootIcons)
            {
                if (icon.ContainerGuid == containerGuid)
                {
                    icon.IsSelected = true;
                    _containerManager.SelectContainer(containerGuid);
                }
                else
                    icon.IsSelected = false;
            }
        }

        public void SetContainerIcons()
        {
            DestroyContainers();
            if (_containerManager.Containers.Count == 0) return;
            foreach (var container in _containerManager.Containers.Values)
            {
                ContainerIcon icon = Instantiate<ContainerIcon>(_lootIconPrefab, transform);
                icon.SetContainerIcon(guid: container.GuId, image: container.Item.Stats.PrimarySprite, containersDisplay: this);
                icon.IsSelected = false;
                lootIcons.Add(icon);
            }
        }

        public void DestroyContainers()
        {
            for (int i = 0; i < lootIcons.Count; i++)
            {
                Destroy(lootIcons[i].gameObject);
            }
            lootIcons.Clear();
        }

        public void OnShow()
        {
            
        }
    }
}
