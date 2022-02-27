using InventoryQuest;
using InventoryQuest.UI;
using UnityEngine;
using Zenject;
using Data;
using InventoryQuest.Managers;

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

    private void Awake()
    {
        _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelectedHandler;
    }

    #region Connect Containers

    public void ConnectCharacterContainer(Container characterContainer)
    {
        characterContainerDisplay.MyContainer = characterContainer;
    }

    public void ConnectLootContainer(Container lootContainer)
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
        var container = _partyManager.CurrentParty.Characters[e.Message].PrimaryContainer;
        if (container is null) return;
        ConnectCharacterContainer(container);
    }
}
