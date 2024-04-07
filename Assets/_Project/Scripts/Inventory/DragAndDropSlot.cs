using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Inventory
{
    [RequireComponent(typeof(MouseFollower))]
    public class DragAndDropSlot : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        private MouseFollower _mouseFollower;
        private Vector2 _startDragPosition;
        private readonly RaycastHit2D[] _casts = new RaycastHit2D[5];
        private InventorySlot _inventorySlot;

        public void Initialize(InventorySlot inventorySlot)
        {
            _inventorySlot = inventorySlot;
            _mouseFollower = GetComponent<MouseFollower>();
            _mouseFollower.Toggle(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _startDragPosition = transform.position;
            _mouseFollower.Toggle(true);
        }

        public void OnDrag(PointerEventData eventData) => _mouseFollower.Follow();

        public void OnEndDrag(PointerEventData eventData)
        {
            _mouseFollower.Toggle(false);
            Physics2D.CircleCastNonAlloc(transform.position, 0.5f, Vector2.one, _casts);
            var blankSlot = FindCastTarget();
            ChangeSlotPosition(blankSlot);
        }

        private BlankSlot FindCastTarget()
        {
            foreach (var cast in _casts.OrderBy(x => x.distance))
            {
                if (cast.collider == null)
                    continue;
                if (cast.collider.gameObject.TryGetComponent<BlankSlot>(out var blankSlot) == false)
                    continue;

                return blankSlot;
            }

            return null;
        }

        private void ChangeSlotPosition(BlankSlot blankSlot)
        {
            if (blankSlot == null)
            {
                transform.position = _startDragPosition;
            }
            else
            {
                var oldBlankSlot = transform.parent.GetComponent<BlankSlot>();
                TrySwapSlots(blankSlot, oldBlankSlot);
                SetInNewPosition(blankSlot);
            }
        }

        private void SetInNewPosition(BlankSlot blankSlot)
        {
            transform.position = blankSlot.transform.position;
            transform.SetParent(blankSlot.transform);
            FindObjectOfType<InventoryManager>().ChangeIndex(_inventorySlot.InventoryItem, blankSlot.Index);
        }

        private void TrySwapSlots(BlankSlot blankSlot, BlankSlot oldBlankSlot)
        {
            var newInventorySlot = blankSlot.GetComponentInChildren<InventorySlot>();

            if (newInventorySlot == null || newInventorySlot == _inventorySlot)
            {
                return;
            }

            var oldBlankSlotTransform = oldBlankSlot.transform;
            newInventorySlot.transform.position = oldBlankSlotTransform.position;
            newInventorySlot.transform.SetParent(oldBlankSlotTransform);
            FindObjectOfType<InventoryManager>().ChangeIndex(newInventorySlot.InventoryItem, oldBlankSlot.Index);
        }
    }
}