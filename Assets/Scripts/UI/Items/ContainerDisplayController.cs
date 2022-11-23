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
            if (Debug.isDebugBuild)
                Debug.Log($"ConnectCharacterContainer called for container {characterContainer.GuId}");
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
            if (Debug.isDebugBuild)
                Debug.Log($"OnPartyMemberSelectedHandler for character {e}");
            var container = _partyManager.CurrentParty.Characters[e].Backpack;
            if (container is not IContainer) {
                if (Debug.isDebugBuild)
                    Debug.LogWarning($"No backpack found for character guid {e}", this);
                return; 
            }
            ConnectCharacterContainer(container);
        }
    }
}