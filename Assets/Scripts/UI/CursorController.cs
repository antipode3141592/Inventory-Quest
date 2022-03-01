using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace InventoryQuest.UI
{
    public class CursorController : MonoBehaviour, IPointerMoveHandler, IPointerUpHandler
    {
        [SerializeField]
        Cursor cursorPrefab;

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
            UnityEngine.Cursor.visible = false;
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
            ShowItemSprite(_gameManager.HoldingItem.Stats.SpritePath);
        }

        public void OnItemPlacedHandler(object sender, EventArgs e)
        {
            HideItemSprite();
        }

        public void ShowItemSprite(string spritePath)
        {
            _cursor.itemIcon.sprite = Resources.Load<Sprite>(spritePath);
            _cursor.itemIcon.color = Color.white;
            _cursor.cursorIcon.color = Color.clear;
        }

        public void HideItemSprite()
        {
            _cursor.itemIcon.sprite = null;
            _cursor.itemIcon.color = Color.clear;
            _cursor.cursorIcon.color = Color.white;
        }
    }
}