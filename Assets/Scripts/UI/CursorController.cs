using Data.Shapes;
using InventoryQuest.Managers;
using System;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI
{
    public class CursorController : MonoBehaviour
    {
        IInputManager _inputManager;

        [SerializeField] Cursor cursorPrefab;
        [SerializeField] bool hardwareCursorEnable;
        [SerializeField] Transform cursorParentTransform;
        [SerializeField] Canvas canvas;
        
        Cursor _cursor;

        [Inject]
        public void Init(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        void Awake()
        {
            _cursor = Instantiate(cursorPrefab, cursorParentTransform);
            UnityEngine.Cursor.visible = hardwareCursorEnable;
            HideItemSprite();
        }

        void Start()
        {
            _inputManager.OnItemHeld += OnItemHeldHandler;
            _inputManager.OnItemPlaced += OnItemPlacedHandler;
            _inputManager.OnRotateCW += OnItemRotateCW;
            _inputManager.OnRotateCCW += OnItemRotateCCW;
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
            _cursor.itemIcon.sprite = _inputManager.HoldingItem.Sprite;
            _cursor.itemIcon.SetNativeSize();
            _cursor.itemIcon.color = new Color(1f, 1f, 1f, 0.75f);
            ImageUtilities.RotateSprite(_inputManager.HoldingItem.CurrentFacing, _cursor.itemIcon);
        }

        public void HideItemSprite()
        {
            _cursor.itemIcon.sprite = null;
            _cursor.itemIcon.color = Color.clear;
        }
    }
}