using Assets.Game.Scripts.System;
using Cysharp.Threading.Tasks;
using ResourceService;
using StorageService;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingFilesLoader : ILoadingOperation
{
    private readonly StorageManager _storageManager;
    private readonly ServerDynamicStorageService _dynamicDataLoader;

    public LoadingFilesLoader(StorageManager storageManager, ServerDynamicStorageService dynamicDataLoader)
    {
        _storageManager = storageManager;
        _dynamicDataLoader = dynamicDataLoader;
    }

    public string Description => "Loading files from server...";

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess?.Invoke(0.5f);
        await _storageManager.LoadFiles();
        await _dynamicDataLoader.Load<ResourceManager>("alldata");
        onProcess?.Invoke(1f);
    }
}
