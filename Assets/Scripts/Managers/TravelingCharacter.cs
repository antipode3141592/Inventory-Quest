using Data.Characters;
using System.Collections;
using UnityEngine;

namespace InventoryQuest.Traveling
{
    public class TravelingCharacter : MonoBehaviour
    {
        public string CharacterId;
        [SerializeField] protected Animator _animator;
        [SerializeField] protected Avatar _avatar;

        SpriteRenderer _spriteRenderer;

        int _idleHash;
        int _moveHash;
        int _attackHash;

        bool isMoving;
        bool isIdle;
        bool isAttacking;

        protected void Awake()
        {
            _idleHash = Animator.StringToHash("Idle");
            _moveHash = Animator.StringToHash("Walking");
            _attackHash = Animator.StringToHash("Attack");

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Move()
        {
            if (isMoving) return;
            //begin walking animation
            _animator.Play(_moveHash);
            isMoving = true;
            isIdle = false;
            isAttacking = false;
        }

        public void Idle()
        {
            if (isIdle) return;
            isMoving = false;
            isAttacking = false;
            //begin idle animation
            _animator.Play(_idleHash);
            isIdle = true;

        }

        public void Attack(int targetPosition)
        {
            isIdle = false;
            if (isAttacking) return;
            isAttacking = true;
            StartCoroutine(AttackAnimation());
        }

        IEnumerator AttackAnimation()
        {
            //attack
            _animator.Play(_attackHash);
            yield return new WaitForSeconds(3f);
            //go back to Idle()
            Idle();
        }

        public void SetSprite(ICharacter character)
        {
            _spriteRenderer.sprite = character.Stats.Portrait;
        }

        public void Show()
        {
            _spriteRenderer.color = Color.white;
        }

        public void Hide()
        {
            _spriteRenderer.color = Color.clear;
        }
    }
}