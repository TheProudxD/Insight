using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Utilites
{
    public class Fading : MonoBehaviour
    {
        private bool _inProcess;
        private Image _hint;
        private float _delay;
        private float _time;
        private float _alpha;

        public float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                _hint.material.SetColor(0,
                    new Color(_hint.material.color.r, _hint.material.color.g, _hint.material.color.b, value));
            }
        }

        public void TryShow()
        {
            if (_inProcess) return;

            StartCoroutine(ShowAndHide());
        }

        private IEnumerator ShowAndHide()
        {
            _inProcess = true;
            Alpha = 0;

            var t = _time;
            while (t > 0)
            {
                Alpha += Time.deltaTime / _time;
                t -= Time.deltaTime;
                yield return null;
            }

            Alpha = 1;
            yield return new WaitForSeconds(_delay);

            t = _time;
            while (t > 0)
            {
                Alpha -= Time.deltaTime / _time;
                t -= Time.deltaTime;
                yield return null;
            }

            Alpha = 0;
            _inProcess = false;
        }
    }
}