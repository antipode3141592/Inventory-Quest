﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InventoryQuest
{
    [CreateAssetMenu(menuName ="ItemShape")]
    public class ItemShape: ScriptableObject
    {
        public Shape Shape;
    }
}
