using DG.Tweening;
using Player;
using UnityEngine;

public class DashAbility : Ability
{
    [SerializeField] private float _dashForce;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private PlayerMovement _playerMovement;

    private Tweener _tweener;

    public override float Use()
    {
        ReloadingDurationTimer = 0;

        _playerRigidbody.AddForce(_playerMovement.GetFaceDirection() * _dashForce, ForceMode2D.Force);
        /*
        var dashVector = (Vector2)_playerRigidbody.transform.position + _playerMovement.GetFaceDirection() * _dashForce;
        _tweener = _playerRigidbody.DOMove(dashVector, Duration).OnComplete(() =>
        {
            //PlayerStateMachine.Current = PlayerState.Idle;
            _tweener = null;
        });
        */
        AbilityAudioPlayer.PlayDashAbilitySound();
        return ReloadingDuration;
    }

    public override bool CanUse() => ReloadingDurationTimer >= ReloadingDuration && _tweener == null;
}