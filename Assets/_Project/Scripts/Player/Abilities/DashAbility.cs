using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Player;
using UnityEngine;

public class DashAbility : Ability
{
    private readonly float _waitDuration = 5;
    
    [SerializeField] private float _dashForce;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private PlayerMovement _playerMovement;

    private float _waitDurationTimer;
    private TweenerCore<Vector2, Vector2, VectorOptions> _tweener;

    private void Awake() => _waitDurationTimer = _waitDuration;

    public override float Use()
    {
        _waitDurationTimer = 0;
        var dashVector = (Vector2)_playerRigidbody.transform.position + _playerMovement.GetFaceDirection() * _dashForce;
        _tweener = _playerRigidbody.DOMove(dashVector, Duration).OnComplete(() =>
        {
            PlayerStateMachine.Current = PlayerState.Idle;
            _tweener = null;
        });

        return _waitDuration;
    }

    private void Update()
    {
        if (_waitDurationTimer <= _waitDuration)
        {
            _waitDurationTimer += Time.deltaTime;
        }
    }

    public override bool CanUse() => _waitDurationTimer >= _waitDuration && _tweener == null;
}