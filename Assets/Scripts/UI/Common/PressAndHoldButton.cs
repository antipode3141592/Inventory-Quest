using InventoryQuest.Managers;
using InventoryQuest.UI.Menus;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class PressAndHoldButton : MonoBehaviour, IOnMenuShow, IOnMenuHide, IPointerDownHandler, IPointerUpHandler
    {
        float timer = 0f;
        float fillRatio = 0f;
        bool selected = false;

        [SerializeField, Range(0f, 1f)] float MinHoldTime = .7f;
        [SerializeField] Color fillColor;
        [SerializeField] Image fillBackground;
        [SerializeField] Image fillForeground;
        [SerializeField] TextMeshProUGUI buttonText;

        public event EventHandler OnPointerHoldSuccess;
        public event EventHandler OnTimerStart;
        public event EventHandler OnTimerReset;

        void Awake()
        {
            fillForeground.color = fillColor;
            timer = 0f;
            fillRatio = 0f;
            SetFill(fillRatio);
        }

        //void Update()
        //{
        //    if (!available) return;
        //    if (Input.GetMouseButton(0) && selected)
        //    {
        //        timer += Time.deltaTime;
        //        if (Debug.isDebugBuild)
        //            Debug.Log($"timer: {timer} of {MinHoldTime}", this);
        //    }
        //    else
        //        ResetTimer();
        //    fillRatio = timer / MinHoldTime;
        //    SetFill(fillRatio);
        //    if (timer >= MinHoldTime)
        //    {
        //        OnPointerHoldSuccess?.Invoke(this, EventArgs.Empty);
        //        ResetTimer();
        //    }
        //}

        //void ResetTimer()
        //{
        //    selected = false;
        //    timer = 0f;
        //}

        //void OnMouseDown()
        //{
        //    if (Debug.isDebugBuild)
        //        Debug.Log($"OnMouseDown() on {gameObject.name}", this);
        //    selected = true;
        //}

        void SetFill(float fillPercentage)
        {
            fillForeground.rectTransform.localScale = new Vector3(x: Mathf.Clamp01(fillPercentage), y: 1f, z: 1f);
        }

        public void UpdateButtonText(string text)
        {
            buttonText.text = text;
        }

        bool available;

        public void OnShow()
        {
            available = true;
        }

        public void OnHide()
        {
            available = false;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"OnPointerDown() on {gameObject.name}", this);
            filling = true;
            StartCoroutine(Filling());
        }

        bool filling;

        public void OnPointerUp(PointerEventData eventData)
        {
            filling = false;
        }

        IEnumerator Filling()
        {
            timer = 0f;
            while(available && filling && Input.GetMouseButton(0))
            {
                timer += Time.deltaTime;
                fillRatio = timer / MinHoldTime;
                SetFill(fillRatio);
                if (timer >= MinHoldTime)
                {
                    OnPointerHoldSuccess?.Invoke(this, EventArgs.Empty);
                    timer = 0f;
                    filling = false;
                }
                yield return null;
            }
            SetFill(0);
        }
    }
}