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
        [SerializeField] 
        public EventHandler OnPointerHoldSuccess;

        private void Update()
        {
            if (!enableTimer) return;
            timer += Time.deltaTime;
            //if timer >= HoldTimer, do the thing
            if (timer < HoldTimer) return;
            OnPointerHoldSuccess?.Invoke(this, EventArgs.Empty);
            enableTimer = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //enable timer;
            enableTimer = true;
            timer = 0f;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //reset timer
            enableTimer = false;
            timer = 0f;

        }
    }
}