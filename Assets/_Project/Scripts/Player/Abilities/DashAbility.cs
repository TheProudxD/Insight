using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Player;
using UnityEngine;

public class DashAbility : Ability
{
    private readonly float _reloadingDuration = 5;
    
    [SerializeField] private float _dashForce;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private PlayerMovement _playerMovement;

    private float _reloadingDurationTimer;
    private TweenerCore<Vector2, Vector2, VectorOptions> _tweener;

    private void Awake() => _reloadingDurationTimer = _reloadingDuration;

    public override float Use()
    {
        _reloadingDurationTimer = 0;
        var dashVector = (Vector2)_playerRigidbody.transform.position + _playerMovement.GetFaceDirection() * _dashForce;
        _tweener = _playerRigidbody.DOMove(dashVector, Duration).OnComplete(() =>
        {
            PlayerStateMachine.Current = PlayerState.Idle;
            _tweener = null;
        });

        return _reloadingDuration;
    }

    private void Update()
    {
        if (_reloadingDurationTimer <= _reloadingDuration)
        {
            _reloadingDurationTimer += Time.deltaTime;
        }
    }

    public override bool CanUse() => _reloadingDurationTimer >= _reloadingDuration && _tweener == null;
}