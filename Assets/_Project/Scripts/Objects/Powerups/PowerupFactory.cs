using System;
using ResourceService;
using UnityEngine;
using Zenject;

namespace Objects.Powerups
{
    public class PowerupFactory : PlaceholderFactory<Powerup, Vector3, Powerup>
    {
        private readonly CoinPowerupEntitySpecs _coinPowerupEntitySpecs;
        private readonly HeartPowerupEntitySpecs _heartPowerupEntitySpecs;
        private readonly ResourceManager _resourceManager;

        public PowerupFactory([Inject(Id = "one coin")] CoinPowerupEntitySpecs coinPowerupEntitySpecs,
            [Inject(Id = "one heart")] HeartPowerupEntitySpecs heartPowerupEntitySpecs, ResourceManager resourceManager)
        {
            _coinPowerupEntitySpecs = coinPowerupEntitySpecs;
            _heartPowerupEntitySpecs = heartPowerupEntitySpecs;
            _resourceManager = resourceManager;
        }

        public override Powerup Create(Powerup powerupPrefab, Vector3 position)
        {
            var powerup = UnityEngine.Object.Instantiate(powerupPrefab, position, Quaternion.identity);
            switch (powerup)
            {
                case Coin coin:
                    coin.Initialize(_coinPowerupEntitySpecs, _resourceManager);
                    return coin;
                case Heart heart:
                    heart.Initialize(_heartPowerupEntitySpecs);
                    return heart;
                default:
                    throw new ArgumentOutOfRangeException(nameof(powerup));
            }
        }
    }
}