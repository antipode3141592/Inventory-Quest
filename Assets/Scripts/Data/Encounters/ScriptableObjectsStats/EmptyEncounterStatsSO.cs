using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Encounters
{
    [CreateAssetMenu(menuName = "InventoryQuest/EncounterStats/Empty", fileName = "empty_")]
    public class EmptyEncounterStatsSO : SerializedScriptableObject, IEmptyEncounterStats
    {
        [SerializeField] protected string id;
        protected string _name = "";
        protected string description = "";
        [SerializeField] protected List<IChoice> choices = new() { new EmptyChoice() };

        public string Id => id;
        public string Name => _name;
        public string Description => description;
        public List<IChoice> Choices => choices;
    }
}