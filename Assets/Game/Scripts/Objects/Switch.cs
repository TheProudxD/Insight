using System;
using Unity.VisualScripting;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private Door _door;
    [SerializeField] private Sprite _activeSprite;
    [SerializeField] private BoolValue _storedValue;
    private bool _isActive;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _isActive = _storedValue.RuntimeValue;
        if (_isActive)
            ActivateSwitch();
    }

    private void ActivateSwitch()
    {
        if (!_door.IsUnityNull()) _door.Open();
        _isActive = true;
        _storedValue.RuntimeValue = _isActive;
        _spriteRenderer.sprite = _activeSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            ActivateSwitch();
        }
    }
}