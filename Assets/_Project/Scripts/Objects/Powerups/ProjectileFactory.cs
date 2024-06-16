using System;
using Enemies;
using ResourceService;
using UnityEngine;
using Zenject;

namespace Objects
{
    public class ProjectileFactory : PlaceholderFactory<Projectile, Vector3, Quaternion, Projectile>
    {
        private readonly DamagerSpecs _damagerSpecs;

        public ProjectileFactory([Inject(Id = "player")] DamagerSpecs damagerSpecs) => _damagerSpecs = damagerSpecs;

        public override Projectile Create(Projectile projectilePrefab, Vector3 position, Quaternion rotation)
        {
            var projectile = UnityEngine.Object.Instantiate(projectilePrefab, position, rotation);
            switch (projectile)
            {
                case RockProjectile rockProjectile:
                    rockProjectile.Initialize(_damagerSpecs, 1, 8);
                    return rockProjectile;
                case BouncyBall bouncyBall:
                    bouncyBall.Initialize(_damagerSpecs, 3, 5);
                    return bouncyBall;
                default:
                    throw new ArgumentOutOfRangeException(nameof(projectile));
            }
        }
    }
}