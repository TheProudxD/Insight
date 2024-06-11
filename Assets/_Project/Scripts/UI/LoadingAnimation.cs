using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Objects
{
    public class LoadingAnimation : MonoBehaviour
    {
        [Inject(Id = "loading image")] private Image _loadingImage;
        private Coroutine _coroutine;

        private IEnumerator Load(Action callback = null, float loadingSpeed = 0.5f)
        {
            while (_loadingImage.fillAmount < 1f)
            {
                _loadingImage.fillAmount += loadingSpeed * Time.deltaTime;
                yield return null;
            }

            _loadingImage.fillAmount = 0f;

            callback?.Invoke();
        }

        public void Reset()
        {
            if (_coroutine == null)
                return;

            StopCoroutine(_coroutine);
            _loadingImage.fillAmount = 0f;
            _coroutine = null;
        }

        public void Animate(Action callback = null, float loadingSpeed = 0.5f) =>
            _coroutine ??= StartCoroutine(Load(callback, loadingSpeed));
    }
}