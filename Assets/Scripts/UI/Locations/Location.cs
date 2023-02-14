using Data.Locations;
using InventoryQuest.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.UI.Locations
{
    public class Location : MonoBehaviour, ILocation
    {
        IInputManager _inputManager;

        [SerializeField] LocationStatsSO locationStats;
        [SerializeField] List<IEnableable> enableables;

        public ILocationStats Stats => locationStats;

        [Inject]
        public void Init(IInputManager inputManager)
        {
            _inputManager = inputManager;
        }

        void Awake()
        {
            enableables = new(GetComponentsInChildren<IEnableable>());
        }

        void Start()
        {
            _inputManager.OpenInventoryCommand += OpenInventoryScreen;
            _inputManager.CloseInventoryCommand += CloseInventoryScreen;
        }

        private void CloseInventoryScreen(object sender, EventArgs e)
        {
            foreach (var enableable in enableables)
                enableable.Enable();
        }

        private void OpenInventoryScreen(object sender, EventArgs e)
        {
            foreach(var enableable in enableables)
                enableable.Disable();
        }
    }
}
