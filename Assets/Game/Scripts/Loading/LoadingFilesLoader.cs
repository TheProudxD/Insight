using Cysharp.Threading.Tasks;
using StorageService;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingFilesLoader : ILoadingOperation
{
    private StorageManager _storageManager;
    public LoadingFilesLoader(StorageManager storageManager) => _storageManager = storageManager;
    public string Description => "Loading files from server...";

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.5f);
        await _storageManager.LoadFiles();
        onProcess?.Invoke(1f);
    }
}
