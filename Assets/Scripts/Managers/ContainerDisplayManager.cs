using InventoryQuest;
using InventoryQuest.UI;
using UnityEngine;
using Zenject;

public class ContainerDisplayManager : MonoBehaviour
{
    GameManager _gameManager;


    [SerializeField]
    ContainerDisplay characterContainerDisplay;
    [SerializeField]
    ContainerDisplay lootContainerDisplay;

    [Inject]
    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
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
}
