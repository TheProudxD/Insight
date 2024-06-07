using UnityEngine;
using Zenject;

public class AppStartUp : MonoBehaviour
{
    [Inject] private LoadingScreenLoader _loadingScreenLoader;
    [Inject] private ILoadingOperation[] _operations;

    private async void Awake() =>
        await _loadingScreenLoader.LoadAndDestroy(_operations);
}