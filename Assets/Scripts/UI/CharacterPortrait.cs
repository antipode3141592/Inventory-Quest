using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterPortrait : MonoBehaviour
{
    [SerializeField]
    Image background;
    [SerializeField]
    Image portrait;
    [SerializeField]
    TextMeshProUGUI nameText;

    //ex path: Portraits/Enemy 01-1  (exclude leading slash and filetype)
    public void SetImage(string path)
    {
        portrait.sprite = Resources.Load<Sprite>(path);
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }
}
