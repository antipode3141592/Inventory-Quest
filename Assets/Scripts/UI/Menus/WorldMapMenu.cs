using Data;
using InventoryQuest.Managers;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryQuest.UI.Menus
{
    public class WorldMapMenu : Menu
    {
        IGameStateDataSource _gameStateDataSource;
        IAdventureManager _adventureManager;

        [Inject]
        public void Init(IGameStateDataSource gameStateDataSource, IAdventureManager adventureManager)
        {
            _gameStateDataSource = gameStateDataSource;
            _adventureManager = adventureManager;
        }

        protected override void Awake()
        {
            base.Awake();
        }

        public override void Show()
        {
            base.Show();
        }

        public override void Hide()
        {
            base.Hide();
        }

        void OnLocationSelectedHandler(object sender, string e)
        {

        }

        public void ChoosePath()
        {
            _gameStateDataSource.SetCurrentLocation("Startington");
            _gameStateDataSource.SetDestinationLocation("Destinationville");
            _adventureManager.Pathfinding.ChoosePath();
        }
    }
}