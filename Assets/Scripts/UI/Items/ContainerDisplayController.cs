using Data.Items;
using InventoryQuest.Managers;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class ContainerDisplayController : MonoBehaviour
    {
        IPartyManager _partyManager;
        IContainerManager _containerManager;

        [SerializeField] ContainerDisplay characterContainerDisplay;
        [SerializeField] ContainerDisplay lootContainerDisplay;


        [Inject]
        public void Init(IPartyManager partyManager, IContainerManager containerManager)
        {
            _partyManager = partyManager;
            _containerManager = containerManager;
        }

        void Start()
        {
            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelectedHandler;

            _containerManager.OnContainerSelected += OnContainerSelectedHandler;
            _containerManager.OnContainersDestroyed += OnContainersDestroyedHandler;
        }

        void OnContainersDestroyedHandler(object sender, EventArgs e)
        {
            DisconnectLootContainer();
        }

        void OnContainerSelectedHandler(object sender, IContainer e)
        {
            ConnectLootContainer(e);
        }

        #region Connect Containers

        public void ConnectCharacterContainer(IContainer characterContainer)
        {
            characterContainerDisplay.SetContainer(characterContainer);
        }

        public void ConnectLootContainer(IContainer lootContainer)
        {
            lootContainerDisplay.SetContainer(lootContainer);
        }

        void DisconnectLootContainer()
        {
            lootContainerDisplay.SetContainer(null);

        }
        #endregion


        void OnPartyMemberSelectedHandler(object sender, string e)
        {
            var container = _partyManager.CurrentParty.Characters[e].Backpack;
            if (container is not IContainer)
                return;
            ConnectCharacterContainer(container);
        }
    }
}