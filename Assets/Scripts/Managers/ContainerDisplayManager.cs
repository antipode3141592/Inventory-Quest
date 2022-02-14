using Data;
using InventoryQuest;
using InventoryQuest.UI;
using Rewired;
using Rewired.Integration.UnityUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class ContainerDisplayManager : MonoBehaviour
{
    GameManager _gameManager;


    [SerializeField]
    ContainerDisplay characterContainerDisplay;
    [SerializeField]
    ContainerDisplay lootContainerDisplay;

    Player _player;
    PlayerMouse _mouse;
    int playerId = 0;

    public PlayerMouse Mouse => _mouse;

    [Inject]
    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public void Awake()
    {
        _player = ReInput.players.GetPlayer(playerId);
        _mouse = PlayerMouse.Factory.Create();
        _mouse.playerId = playerId;

        //default to center of screen
        _mouse.screenPosition = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
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
