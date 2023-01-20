using Data;
using Data.Characters;
using Data.Items;
using Data.Shapes;
using InventoryQuest.Managers;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventoryQuest.Testing
{
    public class InputManagerStub : MonoBehaviour, IInputManager
    {
        public IItem HoldingItem { get; set; }

        public event EventHandler<IItem> OnItemHeld;
        public event EventHandler OnItemPlaced;
        public event EventHandler<RotationEventArgs> OnRotateCW;
        public event EventHandler<RotationEventArgs> OnRotateCCW;
        public event EventHandler OnSubmitDown;
        public event EventHandler OnSubmitHold;
        public event EventHandler OnSubmitUp;
        public event EventHandler CloseInventoryCommand;
        public event EventHandler OpenInventoryCommand;
        public event EventHandler<IItem> ShowItemDetailsCommand;
        public event EventHandler HideItemDetailsCommand;
        public event EventHandler<IItem> ShowHeldItemDetailsCommand;
        public event EventHandler HideHeldItemDetailsCommand;
        public event EventHandler<EncounterModifier> OnEncounterModifierAdded;

        public void CheckRotateAction()
        {
            throw new NotImplementedException();
        }

        public void CheckSubmitAction()
        {
            throw new NotImplementedException();
        }

        public void CloseInventory()
        {
            CloseInventoryCommand?.Invoke(this, EventArgs.Empty);
        }

        public void ContainerDisplayClickHandler(IContainer container, PointerEventData pointerEventData, Coor clickedCoor)
        {
            throw new NotImplementedException();
        }

        public bool EquipmentSlotPointerClickHandler(PointerEventData eventData, ICharacter character, string slotId)
        {
            throw new NotImplementedException();
        }

        public void HideItemDetails()
        {
            throw new NotImplementedException();
        }

        public void OpenInventory()
        {
            OpenInventoryCommand?.Invoke(this, EventArgs.Empty);
        }

        public void ShowItemDetails(IItem item)
        {
            throw new NotImplementedException();
        }
    }
}
