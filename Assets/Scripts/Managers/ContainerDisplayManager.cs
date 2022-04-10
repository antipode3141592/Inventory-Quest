using Data;
using Data.Items;
using InventoryQuest.UI;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Managers
{
    public class ContainerDisplayManager : MonoBehaviour
    {
        PartyManager _partyManager;
        RewardManager _rewardManager;

        [SerializeField]
        ContainerDisplay characterContainerDisplay;
        [SerializeField]
        ContainerDisplay lootContainerDisplay;

        [Inject]
        public void Init(PartyManager partyManager, RewardManager rewardManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
        }

        private void Start()
        {
            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelectedHandler;
            _rewardManager.OnRewardsCleared += OnRewardsClearedHandler;
        }

        private void OnRewardsClearedHandler(object sender, EventArgs e)
        {
            DisconnectLootContainer();
        }

        #region Connect Containers

        public void ConnectCharacterContainer(IContainer characterContainer)
        {
            characterContainerDisplay.MyContainer = characterContainer;
        }

        public void ConnectLootContainer(IContainer lootContainer)
        {
            lootContainerDisplay.MyContainer = lootContainer;
        }

        void DisconnectCharacterContainer()
        {
            characterContainerDisplay.MyContainer = null;
        }

        void DisconnectLootContainer()
        {
            lootContainerDisplay.MyContainer = null;

        }
        #endregion


        void OnPartyMemberSelectedHandler(object sender, MessageEventArgs e)
        {
            var container = _partyManager.CurrentParty.Characters[e.Message].Backpack;
            if (container is null) return;
            ConnectCharacterContainer(container);
        }
    }
}