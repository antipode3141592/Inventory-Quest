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
    public class PressAndHoldButton : MonoBehaviour, ISelectHandler, IDeselectHandler, IOnMenuShow, IOnMenuHide
    {
        IInputManager _inputManager;

        bool selected = false;
        bool filling = false;
        float timer = 0f;
        float fillRatio = 0f;

        int holdCount = 0;

        [SerializeField, Range(0f, 1f)] float MinHoldTime = .7f;
        [SerializeField] Color fillColor;
        [SerializeField] Image fillBackground;
        [SerializeField] Image fillForeground;
        [SerializeField] TextMeshProUGUI buttonText;

        public event EventHandler OnPointerHoldSuccess;
        public event EventHandler OnTimerStart;
        public event EventHandler OnTimerReset;

        Selectable selectable;

        public void Select() => selectable.Select();

        [Inject]
        public void Init(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        void Awake()
        {
            fillForeground.color = fillColor;
            selectable = GetComponent<Selectable>();
            timer = 0f;
            fillRatio = 0f;
            SetFill(fillRatio);
        }

        void SubmitUpHandler(object sender, EventArgs e)
        {
            ResetTimer();
        }

        void SubmitHoldHandler(object sender, EventArgs e)
        {
            if (filling) return;
            if (!selected) return;
            if (holdCount < 3)
            {
                holdCount++;
                return;
            }
            StartCoroutine(Filling());
        }

        void SubmitDownHandler(object sender, EventArgs e)
        {
            
            
        }

        IEnumerator Filling()
        {
            filling = true;
            while (timer < MinHoldTime)
            {
                timer += Time.deltaTime;
                fillRatio = timer / MinHoldTime;
                SetFill(fillRatio);
                yield return null;
            }
            if (filling)
                OnPointerHoldSuccess?.Invoke(this, EventArgs.Empty);
            timer = 0f;
            filling = false;
        }

        void SetFill(float fillPercentage)
        {
            fillForeground.rectTransform.localScale = new Vector3(x: Mathf.Clamp01(fillPercentage), y: 1f, z: 1f);
        }

        public void UpdateButtonText(string text)
        {
            buttonText.text = text;
        }

        public void OnSelect(BaseEventData eventData)
        {
            selected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            selected = false;
            ResetTimer();
        }

        public void OnShow()
        {
            _inputManager.OnSubmitDown += SubmitDownHandler;
            _inputManager.OnSubmitHold += SubmitHoldHandler;
            _inputManager.OnSubmitUp += SubmitUpHandler;
        }

        public void OnHide()
        {
            _inputManager.OnSubmitDown -= SubmitDownHandler;
            _inputManager.OnSubmitHold -= SubmitHoldHandler;
            _inputManager.OnSubmitUp -= SubmitUpHandler;
        }

        void ResetTimer()
        {
            StopAllCoroutines();
            SetFill(0f);
            filling = false;
            timer = 0f;
            holdCount = 0;
        }
    }
}