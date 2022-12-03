using System;
using UnityEngine;
using Zenject;


namespace InventoryQuest.Managers
{
    public class WoodHarvestSawDisplay: MonoBehaviour
    {
        IHarvestManager _harvestManager;

        Animator _animator;

        int __idle;
        int __cut;

        Vector3 initialPosition;
        protected readonly Vector3 offset = new(0f, 10000f, 0f);

        [Inject]
        public void Init(IHarvestManager harvestManager)
        {
            _harvestManager = harvestManager;
        }

        void Awake()
        {
            _animator = GetComponent<Animator>();
            __idle = Animator.StringToHash("Idle");
            __cut = Animator.StringToHash("Cut");
            initialPosition = transform.position;
            Hide();
        }

        void Start()
        {
            _harvestManager.OnItemCut += OnItemCutHandler;
        }

        void OnItemCutHandler(object sender, EventArgs e)
        {
            Cut();
        }

        public void Show()
        {
            transform.position = initialPosition;
        }

        public void Hide()
        {
            transform.position = transform.position + offset;
        }

        public void Cut()
        {
            _animator.SetTrigger(__cut);
        }

        public void Idle()
        {
            _animator.SetTrigger(__idle);
        }
    }
}
