using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.Traveling
{
    public class TravelingPartyController : MonoBehaviour, IPartyController
    {
        [SerializeField] List<TravelingCharacter> partyMembers;
        [SerializeField] TravelSettings travelSettings;

        bool isMoving = false;
        bool isIdle = true;

        public float DistanceMoved { get; protected set; } = 0;

        public event EventHandler<float> TravelPercentageUpdate;

        void Awake()
        {
            isMoving = false;
            isIdle = true;
        }

        public void IdleAll()
        {
            if (isIdle) return;
            isIdle = true;
            isMoving = false;

            foreach (var character in partyMembers)
                character.Idle();
        }

        public void MoveAll()
        {
            if (isMoving) return;
            DistanceMoved = 0f;
            isMoving = true;
            isIdle = false;
            foreach(var character in partyMembers)
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
                float travelDistance = Time.deltaTime * travelSettings.PartyTravelingSpeed;
                transform.position = new(travelDistance + transform.position.x, transform.position.y, transform.position.z);
                DistanceMoved += travelDistance;
                TravelPercentageUpdate?.Invoke(this, DistanceMoved / travelSettings.DefaultDistanceBetweenEncounters);
                yield return null;
            }

        }
    }
}