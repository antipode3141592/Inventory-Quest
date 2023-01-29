using Data;
using Data.Characters;
using Data.Items;
using Data.Shapes;
using System;
using UnityEngine.EventSystems;

namespace InventoryQuest.Managers
{
    public interface IInputManager
    {
        public IItem HoldingItem { get; set; }

        public event EventHandler<IItem> OnItemHeld;
        public event EventHandler OnItemPlaced;
        public event EventHandler<RotationEventArgs> OnRotateCW;
        public event EventHandler<RotationEventArgs> OnRotateCCW;
        public event EventHandler OnSubmitDown;
        public event EventHandler OnSubmitHold;
        public event EventHandler OnSubmitUp;
        public event EventHandler OpenInventoryCommand;
        public event EventHandler CloseInventoryCommand;
        public event EventHandler<IItem> ShowItemDetailsCommand;
        public event EventHandler HideItemDetailsCommand;
        public event EventHandler<EncounterModifier> OnEncounterModifierAdded;
        public event EventHandler<IItem> OnItemUsed;

        public void OpenInventory();
        public void CloseInventory();
        public void CheckRotateAction();
        public void CheckSubmitAction();
        public void ShowItemDetails(IItem item);
        public void HideItemDetails();
        public void ContainerDisplayClickHandler(IContainer container, PointerEventData pointerEventData, Coor clickedCoor);
        public bool EquipmentSlotPointerClickHandler(PointerEventData eventData, ICharacter character, string slotId);
    }
}
