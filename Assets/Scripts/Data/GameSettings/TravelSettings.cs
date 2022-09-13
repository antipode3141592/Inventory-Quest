﻿using UnityEngine;

namespace InventoryQuest
{
    [CreateAssetMenu(menuName = "GameSettings/TravelSettings")]
    public class TravelSettings : ScriptableObject
    {
        public float PartyTravelingSpeed;
        public float DefaultDistanceBetweenEncounters;
    }
}