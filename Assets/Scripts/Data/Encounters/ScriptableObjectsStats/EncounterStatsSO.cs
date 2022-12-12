using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    [CreateAssetMenu(menuName = "InventoryQuest/EncounterStats/Base", fileName = "e_")]
    public class EncounterStatsSO : SerializedScriptableObject, IEncounterStats
    {
        [SerializeField] protected string id;
        [SerializeField] protected string _name;
        [SerializeField, TextArea(1, 5)] protected string description;
        [SerializeField] protected List<IChoice> choices = new();

        public string Id => id;
        public string Name => _name;
        public string Description => description;
        public List<IChoice> Choices => choices;
    }
}
