using System.Collections;
using Player;
using Tools;
using UnityEngine;
using Zenject;

namespace Objects
{
    public class TreasureChest : Interactable
    {
        private const string OPEN_STATE = "opened";

        [Inject] private PlayerInteraction _playerInteraction;
        [SerializeField] private InventoryItem _keyItem;

        private Animator _animator;
        private readonly float _openDuration = 1f;
        private bool _opened;
            
        private void Start()
        {
            _animator = GetComponent<Animator>();
            if (_opened)
                OpenAnimation();
        }

        private IEnumerator OpenChest()
        {
            OpenAnimation();
            _opened = true;
            Context.Raise();
            yield return new WaitForSeconds(_openDuration);
            Context.Raise();
            
            DialogUI.SetText(_keyItem.Description);
            DialogBox.SetActive(true);
            
            _playerInteraction.DisplayPickupItem(_keyItem);
            yield return new WaitUntil(() => Input.anyKey);

            DialogBox.SetActive(false);
            _playerInteraction.RemovePickupItem();
        }

        private void OpenAnimation() => _animator.SetBool(OPEN_STATE, true);

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (_opened)
                return;

            if (InsightUtils.IsItPlayer(other) == false)
                return;
            
            StartCoroutine(OpenChest());
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;

            PlayerInRange = false;
        }
    }
}