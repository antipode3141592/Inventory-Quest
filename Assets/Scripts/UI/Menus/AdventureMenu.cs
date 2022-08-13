using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.UI.Menus
{
    public class AdventureMenu : Menu
    {
        List<IOnMenuShow> menus = new();

        protected override void Awake()
        {
            base.Awake();
            menus.AddRange(GetComponentsInChildren<IOnMenuShow>());
        }

        public override void Show()
        {
            base.Show();
            foreach(var menu in menus)
                menu.OnShow();
        }

        public override void Hide()
        {
            base.Hide();

        }
    }
}
