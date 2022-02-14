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

    private void Update()
    {
        //if (_gameManager.CurrentState == GameStates.HoldingItem)
        //{
        //    Coor currentHoverOver = CursorToCoor(out Coor coor) ? coor : null;

        //    if (currentHoverOver == null || currentHoverOver == lastHoverOver) return;
        //    ResetHighlightStates();

        //    HighlightHeldItemPlacement(currentHoverOver);
        //    currentHoverOver = lastHoverOver;
        //}
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

    //bool CursorToCoor(out Coor coordinates)
    //{
    //    using (var pooledObject = UnityEngine.Pool.ListPool<RaycastHit2D>.Get(out List<RaycastHit2D> hits))
    //    {
    //        Vector2 target = _mouse.screenPosition;
    //        Vector2 worldTarget = _camera.ScreenToWorldPoint(target);
    //        Debug.Log($"target: {target} , worldTarget: {worldTarget}", gameObject);
    //        if (Physics2D.Raycast(worldTarget, -Vector2.up, _contactFilter, hits) >= 1)
    //        {
    //            foreach (var hit in hits)
    //            {
    //                if (hit.transform.gameObject.TryGetComponent<ContainerGridSquareDisplay>(out ContainerGridSquareDisplay square))
    //                {
    //                    coordinates = square.Coordinates;
    //                    return true;

    //                }
    //            }

    //        }
    //        coordinates = null;
    //        return false;
    //    }
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log($"OnPointerDown()", gameObject);
    //}

    //public void OnPointerUp(PointerEventData eventData)
    //{
    //    PlayerPointerEventData playerEventData = (PlayerPointerEventData)eventData;
    //    //Coor target = CursorToCoor(out Coor coor) ? coor : new Coor(-1, -1);
    //    Debug.Log($"processing OnPointerUp() for {gameObject.name}");// at grid {target}");
    //    //switch (_gameManager.CurrentState)
    //    //{
    //    //    case GameStates.Default:

    //    //        if (MyContainer.TryTake(out var item, target))
    //    //        {
    //    //            _gameManager.HoldingItem = item;
    //    //            _gameManager.ChangeState(GameStates.HoldingItem);
    //    //        }
    //    //        break;
    //    //    case GameStates.HoldingItem:
    //    //        if (MyContainer.TryPlace(_gameManager.HoldingItem, target))
    //    //        {
    //    //            _gameManager.HoldingItem = null;
    //    //            _gameManager.ChangeState(GameStates.Default);
    //    //        }
    //    //        break;
    //    //    default:
    //    //        break;
    //    //}
    //}
}
