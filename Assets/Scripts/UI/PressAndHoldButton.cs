using InventoryQuest.Managers;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI
{
    public class PressAndHoldButton : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        IGameManager _gameManager;

        bool selected = false;
        float timer = 0f;
        bool enableTimer = false;
        float fillRatio = 0f;

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
        public void Init(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        void Awake()
        {
            fillForeground.color = fillColor;
            selectable = GetComponent<Selectable>();
            timer = 0f;
            fillRatio = 0f;
            SetFill(fillRatio);
        }

        void Start()
        {
            _gameManager.OnSubmitDown += SubmitDownHandler;
            _gameManager.OnSubmitUp += SubmitUpHandler;
        }

        void SubmitUpHandler(object sender, EventArgs e)
        {
            StopCoroutine(Filling());
            SetFill(0f);
        }

        void SubmitDownHandler(object sender, EventArgs e)
        {
            if (!selected) return;
            StartCoroutine(Filling());
        }

        IEnumerator Filling()
        {
            while (timer < MinHoldTime)
            {
                timer += Time.deltaTime;
                fillRatio = timer / MinHoldTime;
                SetFill(fillRatio);
                yield return null;
            }
            OnPointerHoldSuccess?.Invoke(this, EventArgs.Empty);
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
            StopCoroutine(Filling());
            SetFill(0f);
        }
    }
}