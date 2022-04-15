using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventoryQuest.UI
{
    public class PressAndHoldButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        float timer = 0f;
        [SerializeField] float HoldTimer = 1f;
        bool enableTimer = false;

        public EventHandler OnPointerHoldSuccess;

        private void Update()
        {
            if (!enableTimer) return;
            timer += Time.deltaTime;
            //if timer >= HoldTimer, do the thing
            if (timer < HoldTimer) return;
            OnPointerHoldSuccess?.Invoke(this, EventArgs.Empty);
            Debug.Log("HoldSuccess!");
            enableTimer = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"{gameObject.name} OnPointerClick", this);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log($"{gameObject.name} OnPointerDown...", this);
            //enable timer;
            enableTimer = true;
            timer = 0f;

        }

        public void OnPointerUp(PointerEventData eventData)
        {

            Debug.Log($"{gameObject.name} OnPointerUp", this);
            //reset timer
            enableTimer = false;
            timer = 0f;

        }
    }
}