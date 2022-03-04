using Data;
using System;
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

        float h => _cursor.itemIcon.preferredHeight;
        float w => _cursor.itemIcon.preferredWidth;

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
            //UnityEngine.Cursor.visible = false;
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
            //_cursor.itemIcon.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -90) + _cursor.itemIcon.rectTransform.rotation.eulerAngles);
            RotateSprite(e.TargetFacing);
        }

        public void OnItemRotateCCW(object sender, RotationEventArgs e)
        {
            //_cursor.itemIcon.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 90) + _cursor.itemIcon.rectTransform.rotation.eulerAngles);
            RotateSprite(e.TargetFacing);
        }

        void RotateSprite(Facing TargetFacing)
        {
            switch (TargetFacing)
            {
                case Facing.Right:
                    _cursor.itemIcon.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    _cursor.itemIcon.rectTransform.anchoredPosition = new Vector3(0, 0, 0);
                    break;
                case Facing.Down:
                    _cursor.itemIcon.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    _cursor.itemIcon.rectTransform.anchoredPosition = new Vector3(h,0,0);
                    break;
                case Facing.Left:
                    _cursor.itemIcon.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
                    _cursor.itemIcon.rectTransform.anchoredPosition = new Vector3(w, -h, 0);
                    break;
                case Facing.Up:
                    _cursor.itemIcon.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, -270));
                    _cursor.itemIcon.rectTransform.anchoredPosition = new Vector3(0, -w, 0);
                    break;
                default:
                    break;
            }
        }

        public void ShowItemSprite()
        {
            var item = _gameManager.HoldingItem;
            _cursor.itemIcon.sprite = item.Sprite;
            //set the size
            _cursor.itemIcon.SetNativeSize();
            //determine starting facing
            _cursor.itemIcon.color = Color.white;
            
            RotateSprite(item.Shape.CurrentFacing);
            //_cursor.cursorIcon.color = Color.clear;
        }

        public void HideItemSprite()
        {
            _cursor.itemIcon.sprite = null;
            _cursor.itemIcon.color = Color.clear;
            //_cursor.cursorIcon.color = Color.white;
        }
    }
}