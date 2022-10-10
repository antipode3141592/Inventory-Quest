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
        IGroundController _groundController;
        IEncounterManager _encounterManager;

        [SerializeField] ContainerDisplay characterContainerDisplay;
        [SerializeField] ContainerDisplay lootContainerDisplay;
        [SerializeField] ContainerDisplay groundContainerDisplay;


        [Inject]
        public void Init(IPartyManager partyManager, IRewardManager rewardManager, IGroundController groundController, IEncounterManager encounterManager)
        {
            _partyManager = partyManager;
            _rewardManager = rewardManager;
            _groundController = groundController;
            _encounterManager = encounterManager;
        }

        private void Start()
        {
            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelectedHandler;
            _rewardManager.OnRewardsCleared += OnRewardsClearedHandler;
            _rewardManager.OnLootPileSelected += OnLootPileSelectedHandler;

            _encounterManager.ManagingInventory.StateEntered += OnEncounterPreparationStarted;
        }

        private void OnEncounterPreparationStarted(object sender, EventArgs e)
        {
            if (groundContainerDisplay.isActiveAndEnabled)
                ConnectGroundContainer();
        }

        private void OnLootPileSelectedHandler(object sender, Container e)
        {
            ConnectLootContainer(e);
        }

        private void OnRewardsClearedHandler(object sender, EventArgs e)
        {
            DisconnectLootContainer();
        }

        #region Connect Containers
        public void ConnectGroundContainer()
        {
            groundContainerDisplay.MyContainer = _groundController.GroundContainer;
        }


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


        void OnPartyMemberSelectedHandler(object sender, string e)
        {
            var container = _partyManager.CurrentParty.Characters[e].Backpack;
            if (container is null) return;
            ConnectCharacterContainer(container);
        }
    }
}