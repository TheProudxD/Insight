using Objects;
using Player;
using UnityEngine;
using Zenject;

public class MultiProjectilesAbility : Ability
{
    [Inject] private ProjectileFactory _projectileFactory;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Projectile _projectile;

    [SerializeField] private int _numberOfProjectiles;
    [SerializeField] private float _spread;

    public override float Use()
    {
        var playerFacingDirection = _playerMovement.GetFaceDirection();
        var facingRotation =
            Mathf.Atan2(playerFacingDirection.y, playerFacingDirection.x) * Mathf.Rad2Deg;
        var startRotation = facingRotation + _spread / 2;
        var angleIncrease = _spread / (_numberOfProjectiles - 1);

        for (int i = 0; i < _numberOfProjectiles; i++)
        {
            var tempRotation = startRotation - angleIncrease * i;
            var projectile = _projectileFactory.Create(_projectile, _playerMovement.transform.position,
                Quaternion.Euler(0, 0, tempRotation));
            projectile.Launch(new Vector2(Mathf.Cos(tempRotation * Mathf.Deg2Rad),
                Mathf.Sin(tempRotation * Mathf.Deg2Rad)));
        }

        AbilityAudioPlayer.PlayMultiProjectileAbilitySound();
        return ReloadingDuration;
    }

    public override bool CanUse() => ReloadingDurationTimer >= ReloadingDuration;
}