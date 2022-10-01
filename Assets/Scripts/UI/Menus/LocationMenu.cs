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
    public class LocationMenu : Menu
    {
        IAdventureManager _adventureManager;
        IGameStateDataSource _gameStateDataSource;
        IQuestManager _questManager;

        [SerializeField] List<LocationCharacterPortrait> locationCharacterPortraits;
        [SerializeField] TextMeshProUGUI pathNameText;
        [SerializeField] TextMeshProUGUI locationName;
        [SerializeField] Image locationThumbnailIcon;
        [SerializeField] Image locationBackground;

        [SerializeField] PressAndHoldButton MainMapButton;

        public event EventHandler<string> LocationCharacterSelected;

        [Inject]
        public void Init(IAdventureManager adventureManager, IGameStateDataSource gameStateDataSource, IQuestManager questManager)
        {
            _adventureManager = adventureManager;
            _gameStateDataSource = gameStateDataSource;
            _questManager = questManager;
        }

        protected override void Awake()
        {
            base.Awake();
            _gameStateDataSource.OnCurrentLocationSet += OnCurrentLocationLoadedHandler;
            MainMapButton.OnPointerHoldSuccess += OnMainMapSelected;
        }

        public override void Show()
        {
            base.Show();
            SetupCharacters();
        }

        public override void Hide()
        {
            base.Hide();
            foreach(var character in locationCharacterPortraits)
            {
                if (character.isActiveAndEnabled)
                    character.PortraitSelected -= OnPortraitSelected;
            }
        }

        void OnMainMapSelected(object sender, EventArgs e)
        {
            _adventureManager.Idle.Continue();
        }

        

        void OnCurrentLocationLoadedHandler(object sender, string e)
        {
            var stats = _gameStateDataSource.CurrentLocation.Stats;
            locationName.text = stats.DisplayName;
            Sprite locationIcon = Resources.Load<Sprite>(stats.ThumbnailSpritePath);
            locationThumbnailIcon.sprite = locationIcon;
            SetupCharacters();
        }

        private void SetupCharacters()
        {
            for (int i = 0; i < locationCharacterPortraits.Count; i++)
            {
                if (i < _gameStateDataSource.CurrentLocation.Characters.Count)
                {
                    locationCharacterPortraits[i].gameObject.SetActive(true);
                    locationCharacterPortraits[i].SetUpPortrait(_gameStateDataSource.CurrentLocation.Characters[i]);
                    locationCharacterPortraits[i].PortraitSelected += OnPortraitSelected;
                }
                else
                {
                    locationCharacterPortraits[i].gameObject.SetActive(false);
                }
            }
        }

        void OnPortraitSelected(object sender, string e)
        {
            
            _questManager.EvaluateLocationCharacterQuests(e);
        }

        void StartAdventure(object sender, EventArgs e)
        {
            _adventureManager.Adventuring.StartAdventure();
        }
    }
}