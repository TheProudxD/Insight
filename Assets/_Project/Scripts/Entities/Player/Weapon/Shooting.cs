using UnityEngine;
using Zenject;

namespace Player
{
    public abstract class Shooting : MonoBehaviour
    {
        [Inject] protected PlayerEntitySpecs PlayerEntitySpecs;
        [Inject] protected CharacterAudioPlayer CharacterAudioPlayer;

        protected abstract float TimeBeforeLastAttackCounter { get; set; }
        protected abstract bool CanAttack();

        public abstract bool TryShoot(Vector3 position = default, Vector3 direction = default);
        
        private void Update()
        {
            if (PlayerStateMachine.Current == PlayerState.Interact)
                return;

            if (TimeBeforeLastAttackCounter < PlayerEntitySpecs.SwordAttackCooldown)
            {
                TimeBeforeLastAttackCounter += Time.deltaTime;
            }
        }
    }
}