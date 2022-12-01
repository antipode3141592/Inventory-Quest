using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponProficiencySwitch : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI proficiencyText;

    public event EventHandler ButtonPressed;

    public void UpdateText(string text)
    {
        proficiencyText.text = text;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        ButtonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
