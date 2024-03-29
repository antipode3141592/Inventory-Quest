﻿using UnityEngine;

namespace Data.Items.Components
{
    public class EdibleStats : IUsableStats
    {
        [SerializeField] int restorationStrength;

        public EdibleStats(int restorationStrength)
        {
            this.restorationStrength = restorationStrength;
        }

        public int RestorationStrength => restorationStrength;

        public bool IsConsumable => true;
    }
}
