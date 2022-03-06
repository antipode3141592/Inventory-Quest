using Data;
using Data.Interfaces;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class CursorController : MonoBehaviour, IPointerMoveHandler, IPointerUpHandler
    {
        [SerializeField]
        Cursor cursorPrefab;
        [SerializeField]
        bool hardwareCursorEnable;

        GameManager _gameManager;
        Cursor _cursor;

        [Inject]
        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _cursor = Instantiate<Cursor>(cursorPrefab, transform);
            _gameManager.OnItemHeld += OnItemHeldHandler;
            _gameManager.OnItemPlaced += OnItemPlacedHandler;
            _gameManager.OnRotateCW += OnItemRotateCW;
            _gameManager.OnRotateCCW += OnItemRotateCCW;
            UnityEngine.Cursor.visible = hardwareCursorEnable;
            HideItemSprite();
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            _cursor.transform.position = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            switch (_gameManager.CurrentState)
            {
                case GameStates.Loading:
                    break;
                case GameStates.Default:
                    break;
                case GameStates.HoldingItem:
                    break;
                default:
                    break;
            }
        }

        public void OnItemHeldHandler(object sender, EventArgs e)
        {
            ShowItemSprite();
        }

        public void OnItemPlacedHandler(object sender, EventArgs e)
        {
            HideItemSprite();
        }

        public void OnItemRotateCW(object sender, RotationEventArgs e)
        {
            ImageUtilities.RotateSprite(e.TargetFacing, _cursor.itemIcon);
        }

        public void OnItemRotateCCW(object sender, RotationEventArgs e)
        {
            ImageUtilities.RotateSprite(e.TargetFacing, _cursor.itemIcon);
        }

        public void ShowItemSprite()
        {
            var item = _gameManager.HoldingItem;
            _cursor.itemIcon.sprite = item.Sprite;
            _cursor.itemIcon.SetNativeSize();
            _cursor.itemIcon.color = Color.white;
            ImageUtilities.RotateSprite(item.Shape.CurrentFacing, _cursor.itemIcon);
        }

        public void HideItemSprite()
        {
            _cursor.itemIcon.sprite = null;
            _cursor.itemIcon.color = Color.clear;
        }
    }
}