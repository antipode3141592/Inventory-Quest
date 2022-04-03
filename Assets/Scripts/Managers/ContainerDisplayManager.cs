using Data;
using Data.Interfaces;
using InventoryQuest.Managers;
using InventoryQuest.UI;
using UnityEngine;
using Zenject;

public class ContainerDisplayManager : MonoBehaviour
{
    PartyManager _partyManager;

    [SerializeField]
    ContainerDisplay characterContainerDisplay;
    [SerializeField]
    ContainerDisplay lootContainerDisplay;

    [Inject]
    public void Init(PartyManager partyManager)
    {
        _partyManager = partyManager;
    }

    private void Start()
    {
        _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelectedHandler;
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

    public void DisconnectCharacterContainer()
    {
        characterContainerDisplay = null;
    }

    public void DisconnectLootContainer()
    {
        lootContainerDisplay = null;

    }
    #endregion


    void OnPartyMemberSelectedHandler(object sender, MessageEventArgs e)
    {
        var container = _partyManager.CurrentParty.Characters[e.Message].Backpack;
        if (container is null) return;
        ConnectCharacterContainer(container);
    }
}
