using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private float _speedBar;
    private Camera _uiCamera;
    private float _targetProgress;

    [Inject]
    private void Construct(Camera uiCamera) => _uiCamera = uiCamera;

    public async UniTask Load(ILoadingOperation[] queue)
    {
        if (queue == null)
            throw new ArgumentNullException(nameof(queue));
        DontDestroyOnLoad(this);
        _canvas.worldCamera = _uiCamera;
        _canvas.enabled = true;
        StartCoroutine(UpdateSlider());

        foreach (var operation in queue)
        {
            ResetFill();
            _text.text = operation.Description;
            await operation.Load(OnProgress);
            await Wait();
        }

        _canvas.enabled = false;
    }

    private async UniTask Wait()
    {
        while (_slider.value < _targetProgress)
        {
            await UniTask.Yield();
        }

        //await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
    }

    private void OnProgress(float value)
    {
        _targetProgress = value;
    }

    private void ResetFill()
    {
        _slider.value = 0;
        _targetProgress = 0;
    }

    private IEnumerator UpdateSlider()
    {
        while (_canvas.enabled)
        {
            if (_slider.value < _targetProgress)
                _slider.value += Time.deltaTime * _speedBar;
            yield return null;
        }
    }
}