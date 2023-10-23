using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
public class LoadingScreenLoader : AssetLoader
{
    public async UniTask LoadAndDestroy(ILoadingOperation[] loadingOperations)
    {
        var loadingScreen = await LoadAsync<LoadingScreen>(AddressablePaths.LOADING_SCREEN);

        await loadingScreen.Load(loadingOperations);
        Unload();
    }

    private void Unload()
    {
        if (_cachedObject == null)
            return;
        _cachedObject.SetActive(false);
        Addressables.ReleaseInstance(_cachedObject);
        _cachedObject = null;
    }
}
