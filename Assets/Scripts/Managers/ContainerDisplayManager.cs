using InventoryQuest;
using InventoryQuest.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerDisplayManager : MonoBehaviour
{

    [SerializeField]
    ContainerDisplay characterContainerDisplay;
    [SerializeField]
    ContainerDisplay lootContainerDisplay;




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
}
