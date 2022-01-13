using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InventoryQuest.Characters
{
    //characters 
    public class Character
    {
        public CharacterStats CharacterStats;
        
        public Container PrimaryContainer { get; set; }

        public Character(Container container, CharacterStats stats)
        {
            PrimaryContainer = container;
            CharacterStats = stats;
        }

        public float CurrentEncumbrance => PrimaryContainer.TotalWeight;
        public float CurrentTotalGoldValue => PrimaryContainer.TotalWorth;

    }
}
