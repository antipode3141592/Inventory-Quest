using Data.Items;
using Data.Shapes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace InventoryQuest.Managers
{
    public interface IInputManager
    {
        public IItem HoldingItem { get; set; }

        public event EventHandler OnItemHeld;
        public event EventHandler OnItemPlaced;
        public event EventHandler<RotationEventArgs> OnRotateCW;
        public event EventHandler<RotationEventArgs> OnRotateCCW;
        public event EventHandler OnSubmitDown;
        public event EventHandler OnSubmitHold;
        public event EventHandler OnSubmitUp;
    }
}
