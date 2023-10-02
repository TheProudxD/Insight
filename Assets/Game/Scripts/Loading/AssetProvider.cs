using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

public class AssetLoader
{
    protected GameObject _cachedObject;
    protected async UniTask<T> LoadAsync<T>(string name)
    where T : Object
    {
        var value = Addressables.InstantiateAsync(name);
        _cachedObject = await value;
        if (_cachedObject.TryGetComponent<T>(out var prefab))
            return prefab;
        throw new ArgumentException("Type is not exist");
    }
}
