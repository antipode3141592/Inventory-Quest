using UnityEngine;

[CreateAssetMenu(menuName = "InventoryQuest/Item/Tags", fileName = "tag_")]
public class Tag : ScriptableObject
{
    [SerializeField] string tagName;
    [SerializeField] string description;

    public string TagName { get; }
    public string Description { get; }
}
