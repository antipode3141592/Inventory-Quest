using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace InventoryQuest.Managers
{
    public class WoodHarvestSawDisplay: MonoBehaviour
    {
        IHarvestManager _harvestManager;
        Image _image;
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
            _image = GetComponent<Image>();
            __idle = Animator.StringToHash("Idle");
            __cut = Animator.StringToHash("Cut");
            initialPosition = _image.rectTransform.position;
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

        public void SetInitialPosition(Vector3 position)
        {
            initialPosition = position;
        }

        public void Show()
        {
            Debug.Log($"setting {this.name} to position: {initialPosition}");
            _image.rectTransform.localPosition = initialPosition;
        }

        public void Hide()
        {
            _image.rectTransform.localPosition = _image.rectTransform.position + offset;
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
