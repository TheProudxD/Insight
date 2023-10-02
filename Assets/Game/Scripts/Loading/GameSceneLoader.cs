using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLoader : ILoadingOperation
{
    public string Description => "Game Scene loading...";

    public async UniTask Load(Action<float> onProcess)
    {
        onProcess.Invoke(0.5f);
        SceneManager.LoadScene(SceneNames.MAIN_MENU_SCENE);
        await UniTask.Delay(TimeSpan.FromSeconds(0.15f));
        onProcess.Invoke(1f);
    }
}
