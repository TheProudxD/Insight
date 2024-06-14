using System;
using DG.Tweening;
using Tools;
using UnityEngine;

public class WorldInventoryItem : MonoBehaviour
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private InventoryItem _pickupItem;
    [SerializeField] private int _amountToAdd;

    private void Awake()
    {
        var defPos = (Vector2)transform.localPosition;
        transform.DOLocalMoveY(0.01f, 2)
            //.SetEase(Ease.InSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (InsightUtils.IsItPlayer(other) == false)
            return;

        AddItemToInventory();
        Destroy(gameObject);
    }

    private void AddItemToInventory()
    {
        if (_playerInventory.InventoryItems.Contains(_pickupItem) == false)
        {
            _playerInventory.InventoryItems.Add(_pickupItem);
            _pickupItem.StartAmount(1);
        }

        _pickupItem.Add(_amountToAdd);
    }
}