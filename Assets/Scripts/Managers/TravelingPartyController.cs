using InventoryQuest.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace InventoryQuest.Traveling
{
    public class TravelingPartyController : MonoBehaviour, IPartyController
    {
        IPartyManager _partyManager;

        [SerializeField] List<TravelingCharacter> partyMembers;
        [SerializeField] TravelSettings travelSettings;

        bool isMoving = false;
        bool isIdle = true;

        public float DistanceMoved { get; protected set; } = 0;

        [Inject]
        public void Init(IPartyManager partyManager)
        {
            _partyManager = partyManager;
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
            int partyCount = _partyManager.CurrentParty.Characters.Count;
            for (int i = 0; i < partyMembers.Count; i++)
            {
                if (i < partyCount)
                {
                    partyMembers[i].gameObject.SetActive(true);
                    
                }
                    
                
            }
        }

        public void IdleAll()
        {
            isIdle = true;
            isMoving = false;
            foreach (var character in partyMembers.FindAll(x => x.isActiveAndEnabled))
                character.Idle();
        }

        public void MoveAll()
        {
            if (isMoving) return;
            DistanceMoved = 0f;
            isMoving = true;
            isIdle = false;
            foreach(var character in partyMembers.FindAll(x => x.isActiveAndEnabled))
                character.Move();

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