using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Objects
{
    public class LoadingAnimation : MonoBehaviour
    {
        private readonly float _loadingSpeed = 0.5f;
        
        [Inject(Id = "loading image")] private Image _loadingImage;
        private Coroutine _coroutine;

        private IEnumerator Load(Action callback = null)
        {
            while (_loadingImage.fillAmount < 1f)
            {
                _loadingImage.fillAmount +=  _loadingSpeed*Time.deltaTime;
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

        public void Animate(Action callback = null) => _coroutine ??= StartCoroutine(Load(callback));
    }
}