using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncounterChoiceButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] Button button;

    public Button Button => button;

    public void ChangeButtonText(string text)
    {
        buttonText.text = text;
    }
}
