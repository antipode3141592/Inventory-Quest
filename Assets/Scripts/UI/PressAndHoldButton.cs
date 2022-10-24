using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Rewired;
using InventoryQuest.UI.Menus;

namespace InventoryQuest.UI
{
    public class PressAndHoldButton : MonoBehaviour, ISelectHandler, IDeselectHandler
    {
        Player player;
        int playerId = 0;

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

        void Awake()
        {
            player = ReInput.players.GetPlayer(playerId);
            fillForeground.color = fillColor;

            selectable = GetComponent<Selectable>();

            timer = 0f;
            enableTimer = false;
            fillRatio = 0f;
            SetFill(fillRatio);
        }

        void Update()
        {
            if (!selected) return;

            if (player.GetButtonDown("UISubmit"))
                EnableTimer();
            else if (player.GetButtonUp("UISubmit"))
                ResetTimer();
            else if (player.GetButton("UISubmit"))
            {
                if (!enableTimer) return;

                timer += Time.deltaTime;

                fillRatio = timer / MinHoldTime;
                //apply percentage to fillForeground
                SetFill(fillRatio);
                if (timer < MinHoldTime) return;

                OnPointerHoldSuccess?.Invoke(this, EventArgs.Empty);
                enableTimer = false;
            }
        }

        void SetFill(float fillPercentage)
        {
            fillForeground.rectTransform.localScale = new Vector3(x: Mathf.Clamp01(fillPercentage), y: 1f, z: 1f);
        }

        void EnableTimer()
        {
            enableTimer = true;
            timer = 0f;
            OnTimerStart?.Invoke(this, EventArgs.Empty);
        }

        void ResetTimer()
        {
            enableTimer = false;
            timer = 0f;
            SetFill(0f);
            OnTimerReset?.Invoke(this, EventArgs.Empty);
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
    }
}