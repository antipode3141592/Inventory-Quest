using Data.Shapes;
using InventoryQuest.Managers;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace InventoryQuest.UI
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] Cursor cursorPrefab;
        [SerializeField] bool hardwareCursorEnable;
        [SerializeField] Transform cursorParentTransform;
        [SerializeField] Canvas canvas;
        IGameManager _gameManager;
        Cursor _cursor;

        [Inject]
        public void Init(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private void Awake()
        {
            _cursor = Instantiate<Cursor>(cursorPrefab, cursorParentTransform);
            _gameManager.OnItemHeld += OnItemHeldHandler;
            _gameManager.OnItemPlaced += OnItemPlacedHandler;
            _gameManager.OnRotateCW += OnItemRotateCW;
            _gameManager.OnRotateCCW += OnItemRotateCCW;
            UnityEngine.Cursor.visible = hardwareCursorEnable;
            HideItemSprite();
        }

        void Update()
        {
            _cursor.RectTransform.position = Input.mousePosition;
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
            //set scale
            _cursor.itemIcon.SetNativeSize();
            //var itemPPU = item.Sprite.pixelsPerUnit;
            //float scaleFactor = itemPPU / canvas.referencePixelsPerUnit;
            //Debug.Log($"scaleFactor : {scaleFactor}");
            //_cursor.itemIcon.rectTransform.sizeDelta = new(canvas.referencePixelsPerUnit, canvas.referencePixelsPerUnit);


            _cursor.itemIcon.color = Color.white;
            ImageUtilities.RotateSprite(item.CurrentFacing, _cursor.itemIcon);
        }

        public void HideItemSprite()
        {
            _cursor.itemIcon.sprite = null;
            _cursor.itemIcon.color = Color.clear;
        }
    }
}