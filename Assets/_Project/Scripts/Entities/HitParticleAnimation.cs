using UnityEngine;

namespace Managers
{
    public class HitParticleAnimation : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _hitParticles;

        public void Hit(Vector2 position)
        {
            var item = _hitParticles.GetRandom();
            if (item == null)
                return;

            var particle = Instantiate(item, position, Quaternion.identity);
            if (particle != null)
                particle.Play();
        }
    }
}