using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Player;
using UnityEngine;

public class DashAbility : Ability
{
    [SerializeField] private float _dashForce;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private PlayerMovement _playerMovement;

    private TweenerCore<Vector2, Vector2, VectorOptions> _tweener;

    public override float Use()
    {
        ReloadingDurationTimer = 0;
        var dashVector = (Vector2)_playerRigidbody.transform.position + _playerMovement.GetFaceDirection() * _dashForce;
        _tweener = _playerRigidbody.DOMove(dashVector, Duration).OnComplete(() =>
        {
            PlayerStateMachine.Current = PlayerState.Idle;
            _tweener = null;
        });

        return ReloadingDuration;
    }

    public override bool CanUse() => ReloadingDurationTimer >= ReloadingDuration && _tweener == null;
}