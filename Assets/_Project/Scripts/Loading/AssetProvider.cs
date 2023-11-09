using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class AssetLoader
{
    protected GameObject CachedObject;

    protected async UniTask<T> LoadAsync<T>(string name)
        where T : Object
    {
        var value = Addressables.InstantiateAsync(name);
        CachedObject = await value;
        if (CachedObject.TryGetComponent<T>(out var prefab))
            return prefab;
        throw new ArgumentException("Type is not exist");
    }
}