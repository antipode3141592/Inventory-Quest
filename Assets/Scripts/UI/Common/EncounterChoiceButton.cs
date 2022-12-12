using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncounterChoiceButton : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI descriptiveText;
    [SerializeField] TextMeshProUGUI rulesText;
    [SerializeField] Button button;

    int index = -1;

    public event EventHandler<int> ChoiceMade;

    void Start()
    {
        button.onClick.AddListener(buttonClicked);
    }

    private void buttonClicked()
    {
        ChoiceMade?.Invoke(this, index);
    }

    public Button Button => button;

    public EncounterChoiceButton ChangeButtonText(string descriptiveText, string rulesText)
    {
        this.descriptiveText.text = descriptiveText;
        this.rulesText.text = rulesText;
        return this;
    }

    public EncounterChoiceButton SetIndex(int index)
    {
        this.index = index;
        return this;
    }

    
}
