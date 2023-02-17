using Data.Characters;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace InventoryQuest.UI
{
    public class LocationCharacterPortrait: MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        ICharacter _character;

        [SerializeField] TextMeshProUGUI characterNameText;
        [SerializeField] Image characterPortrait;
        [SerializeField] Image backgroundImage;

        public event EventHandler<string> PortraitSelected;

        public void SetUpPortrait(ICharacter character)
        {
            _character = character;

            characterNameText.text = character.DisplayName;
            characterPortrait.sprite = character.Stats.Portrait;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            PortraitSelected?.Invoke(this, _character.GuId);
        }
    }
}
