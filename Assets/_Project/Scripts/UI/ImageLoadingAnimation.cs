using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class ImageLoadingAnimation : MonoBehaviour
    {
        private IEnumerator Load(Image loadingImage, float loadingSpeed, Action callback = null)
        {
            loadingImage.fillAmount = 0f;

            while (loadingImage.fillAmount < 1f)
            {
                loadingImage.fillAmount += loadingSpeed * Time.deltaTime;
                yield return null;
            }

            loadingImage.fillAmount = 1f;

            callback?.Invoke();
        }

        public void Animate(Image loadingImage, float loadingSpeed, Action callback = null) =>
            StartCoroutine(Load(loadingImage, loadingSpeed, callback));
    }
}