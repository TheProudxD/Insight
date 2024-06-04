using System.Collections;
using UnityEngine;

namespace Player
{
    public class HitAnimation : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteAnimate;
        
        private readonly Color _flashColor = Color.red;
        private readonly Color _regularColor = Color.white;
        private readonly float _flashDuration = 0.04F;
        private readonly int _numberOfFlashes = 5;

        private IEnumerator FlashCo()
        {
            int temp = 0;
            while (temp < _numberOfFlashes)
            {
                _spriteAnimate.color = _flashColor;
                yield return new WaitForSeconds(_flashDuration);
                _spriteAnimate.color = _regularColor;
                yield return new WaitForSeconds(_flashDuration);
                temp++;
            }
        }

        public void Play() => StartCoroutine(FlashCo());
    }
}