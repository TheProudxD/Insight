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
        if (CachedObject == null)
            return;
        CachedObject.SetActive(false);
        Addressables.ReleaseInstance(CachedObject);
        CachedObject = null;
    }
}