using Data;
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
        IRewardManager _rewardManager;

        [SerializeField] ContainerDisplay characterContainerDisplay;
        [SerializeField] ContainerDisplay lootContainerDisplay;


        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
        }

        void Start()
        {
            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelectedHandler;
            _rewardManager.OnRewardsCleared += OnRewardsClearedHandler;
            _rewardManager.OnPileSelected += OnLootPileSelectedHandler;
        }

        void OnLootPileSelectedHandler(object sender, IContainer e)
        {
            ConnectLootContainer(e);
        }

        void OnRewardsClearedHandler(object sender, EventArgs e)
        {
            DisconnectLootContainer();
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

        void DisconnectCharacterContainer()
        {
            characterContainerDisplay.SetContainer(null);
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