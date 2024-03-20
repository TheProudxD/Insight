using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Objects
{
    public class LoadingAnimation : MonoBehaviour
    {
        [Inject] private Hud _hud;

        private Image _loadingBar;
        private WaitForSecondsRealtime _timer;
        private float _loadingSpeed = 0.15f;
        private Coroutine _coroutine;

        private void Awake()
        {
            _loadingBar = _hud.LoadingBar;
            _timer = new WaitForSecondsRealtime(_loadingSpeed);
        }

        private IEnumerator Load(Action callback = null)
        {
            while (_loadingBar.fillAmount < 1f)
            {
                _loadingBar.fillAmount += 0.1f;
                yield return _timer;
            }

            _loadingBar.fillAmount = 0f;

            callback?.Invoke();
        }

        public void Reset()
        {
            if (_coroutine == null)
                return;

            StopCoroutine(_coroutine);
            _loadingBar.fillAmount = 0f;
            _coroutine = null;
        }

		public void Animate(Action callback)
		{
            if (_coroutine==null)
			    _coroutine = StartCoroutine(Load(callback));
		}
	}
}