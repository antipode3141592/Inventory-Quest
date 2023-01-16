using InventoryQuest.Managers;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Traveling
{
    public class TravelingPartyController : SerializedMonoBehaviour, IPartyController
    {
        IPartyManager _partyManager;
        IEncounterManager _encounterManager;

        [SerializeField] Dictionary<string, TravelingCharacter> partyMembers;
        [SerializeField] TravelSettings travelSettings;

        bool isMoving = false;
        bool isIdle = true;

        public float DistanceMoved { get; protected set; } = 0;

        [Inject]
        public void Init(IPartyManager partyManager, IEncounterManager encounterManager)
        {
            _partyManager = partyManager;
            _encounterManager = encounterManager;
        }

        void Awake()
        {
            isMoving = false;
            isIdle = true;
        }

        void Start()
        {
            _partyManager.CurrentParty.OnPartyMemberSelected += OnPartyMemberSelected;
            _partyManager.CurrentParty.OnPartyCompositionChanged += OnPartyCompositionChangedHandler;
            _encounterManager.Wayfairing.StateEntered += OnWayfairingHandler;
            SetPortraits();
        }

        void OnWayfairingHandler(object sender, EventArgs e)
        {
            SetPortraits();
        }

        void OnPartyCompositionChangedHandler(object sender, EventArgs e)
        {
            SetPortraits();
        }

        void OnPartyMemberSelected(object sender, string e)
        {
            SetPortraits();
        }

        public void SetPortraits()
        {
            ClearPortraits();
            foreach(var character in _partyManager.CurrentParty.Characters)
                foreach (var partyMember in partyMembers)
                    if (partyMember.Key.Equals(character.Value.Stats.Id, StringComparison.CurrentCultureIgnoreCase))
                        partyMember.Value.Show();
        }

        void ClearPortraits()
        {
            foreach (var partyMember in partyMembers)
                partyMember.Value.Hide();
        }

        public void IdleAll()
        {
            isIdle = true;
            isMoving = false;
            foreach (var character in partyMembers)
            {
                if (character.Value.isActiveAndEnabled)
                    character.Value.Idle();
            }
        }

        public void MoveAll()
        {
            if (isMoving) return;
            DistanceMoved = 0f;
            isMoving = true;
            isIdle = false;
            foreach (var character in partyMembers)
            {
                if(character.Value.isActiveAndEnabled)
                    character.Value.Move();
            }
            StartCoroutine(Movement());
        }

        public void PauseAll()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator Movement()
        {
            while (isMoving)
            {
                float travelDistance = Time.deltaTime * travelSettings.PartyWalkingSpeed;
                transform.position = new(travelDistance + transform.position.x, transform.position.y, transform.position.z);
                DistanceMoved += travelDistance;
                yield return null;
            }

        }
    }
}