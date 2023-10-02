using StorageService;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AppStartUp : MonoBehaviour
{
    [Inject] private LoadingScreenLoader _loadingScreenLoader;
    [Inject] private ILoadingOperation[] _operations;
    private async void Start()
    {
        Debug.Log(_loadingScreenLoader);
        foreach (var item in _operations)
        {
            Debug.Log(item.GetType());
        }
        await _loadingScreenLoader.LoadAndDestroy(_operations);
    }
}
