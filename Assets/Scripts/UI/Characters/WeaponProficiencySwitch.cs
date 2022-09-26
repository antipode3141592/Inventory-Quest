using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponProficiencySwitch : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI proficiencyText;

    public event EventHandler buttonPressed;

    public void UpdateText(string text)
    {
        proficiencyText.text = text;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
