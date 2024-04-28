using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class AbilityLoadingAnimation : MonoBehaviour
    {
        private Coroutine _coroutine;
        private IEnumerator Load(Image loadingImage, float loadingSpeed, Action callback = null)
        {
            loadingImage.fillAmount = 0f;

            while (loadingImage.fillAmount < 1f)
            {
                loadingImage.fillAmount += loadingSpeed * Time.deltaTime;
                yield return null;
            }

            loadingImage.fillAmount = 1f;
            _coroutine = null;
            
            callback?.Invoke();
        }

        public void Animate(Image loadingImage, float loadingSpeed, Action callback = null) =>_coroutine??=
            StartCoroutine(Load(loadingImage, loadingSpeed, callback));
    }
}