using UnityEngine;

namespace Managers
{
    public interface IKnockbackable
    {
        public void Hit(Vector3 position, float knockTime, float damage);
    }
}