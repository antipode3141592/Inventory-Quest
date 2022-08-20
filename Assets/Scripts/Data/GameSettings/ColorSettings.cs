using UnityEngine;


[CreateAssetMenu(menuName = "GameSettings/ColorSettings")]
public class ColorSettings : ScriptableObject
{
    public Color BuffColor;
    public Color DebuffColor;

    public Color HighlightColor;
    public Color MatchColor;

    public Color TextBuffColor = Color.cyan;
    public Color TextDeBuffColor = Color.red;
}
