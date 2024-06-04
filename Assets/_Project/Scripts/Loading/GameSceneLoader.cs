using Cysharp.Threading.Tasks;
using System;
using Storage;
using UnityEngine.SceneManagement;
using Scene = Storage.Scene;
using SceneManager = UnityEngine.SceneManagement.SceneManager;

public class GameSceneLoader : ILoadingOperation
{
    public string Description => "Game Scene loading...";

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess.Invoke(0.5f);
        SceneManager.LoadScene((int)Scene.Loader);
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        onProcess.Invoke(1f);
    }
}