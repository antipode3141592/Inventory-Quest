using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace InventoryQuest.UI
{
    public class CursorController : MonoBehaviour, IPointerMoveHandler
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
            UnityEngine.Cursor.visible = false;
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            _cursor.transform.position = eventData.position;
        }
    }
}