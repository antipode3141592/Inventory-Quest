using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponProficiencySwitch : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI proficiencyText;
    [SerializeField] Button button;

    public event EventHandler buttonPressed;

    void Awake()
    {
        button.onClick.AddListener(ButtonOnPointerUp);
    }

    void ButtonOnPointerUp()
    {
        buttonPressed?.Invoke(this, EventArgs.Empty);
    }

    public void UpdateText(string text)
    {
        proficiencyText.text = text;
    }
}
