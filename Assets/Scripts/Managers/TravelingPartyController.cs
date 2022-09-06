using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryQuest.Traveling
{
    public class TravelingPartyController : MonoBehaviour, IPartyController
    {
        [SerializeField] List<TravelingCharacter> partyMembers;
        [SerializeField] protected float _speed = 3f;

        bool isMoving = false;
        bool isIdle = true;

        public float DistanceMoved { get; protected set; } = 0;

        private void Awake()
        {
            isMoving = false;
            isIdle = true;
        }

        public void IdleAll()
        {
            if (isIdle) return;
            Debug.Log($"IdleAll() called...");
            isIdle = true;
            isMoving = false;

            foreach (var character in partyMembers)
                character.Idle();
        }

        public void MoveAll()
        {
            if (isMoving) return;
            Debug.Log($"MoveAll() called...");
            DistanceMoved = 0f;
            isMoving = true;
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
                float travelDistance = Time.deltaTime * _speed;
                transform.position = new(travelDistance + transform.position.x, transform.position.y, transform.position.z);
                DistanceMoved += travelDistance;
                yield return null;
            }

        }
    }
}