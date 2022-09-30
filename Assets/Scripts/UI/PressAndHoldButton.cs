using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class PressAndHoldButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        float timer = 0f;
        bool enableTimer = false;
        float fillRatio = 0f;

        [SerializeField, Range(0f, 1f)] float MinHoldTime = .7f;
        [SerializeField] Color fillColor;
        [SerializeField] Image fillBackground;
        [SerializeField] Image fillForeground;
        [SerializeField] public event EventHandler OnPointerHoldSuccess;
        [SerializeField] public event EventHandler OnTimerStart;
        [SerializeField] public event EventHandler OnTimerReset;


        private void Awake()
        {
            fillForeground.color = fillColor;
        }

        void OnEnable()
        {
            timer = 0f;
            enableTimer = false;
            fillRatio = 0f;
            SetFill(fillRatio);
        }

        void Update()
        {
            if (!enableTimer) return;
            timer += Time.deltaTime;
            //calculate percent complete
            fillRatio = timer/MinHoldTime;
            //apply percentage to fillForeground
            SetFill(fillRatio);

            //if timer >= HoldTimer, do the thing
            if (timer < MinHoldTime) return;
            OnPointerHoldSuccess?.Invoke(this, EventArgs.Empty);
            enableTimer = false;
        }

        void SetFill(float fillPercentage)
        {
            fillForeground.rectTransform.localScale = new Vector3(x: Mathf.Clamp01(fillPercentage), y: 1f, z: 1f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //enable timer;
            enableTimer = true;
            timer = 0f;
            OnTimerStart?.Invoke(this, EventArgs.Empty);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //reset timer
            enableTimer = false;
            timer = 0f;
            SetFill(0f);
            OnTimerReset?.Invoke(this, EventArgs.Empty);
        }
    }
}