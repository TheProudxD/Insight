using UnityEngine;

namespace Managers
{
    public interface IDamageable
    {
        public void Hit(Vector3 position, float knockTime, float damage);
    }
}