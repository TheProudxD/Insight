using StorageService;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GlobalInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        var camera = FindObjectOfType<Camera>();
        DontDestroyOnLoad(camera);
        Container.Bind<Camera>().FromInstance(camera).AsSingle();
        Container.Bind<StorageManager>().AsSingle();
        Container.BindInterfacesTo<JsonToFileStorageService>().AsSingle();
        Container.BindInterfacesTo<GameSceneLoader>().AsTransient();
        Container.Bind<LoadingScreenLoader>().AsSingle();
        Container.BindInterfacesTo<LoadingFilesLoader>().AsSingle();
    }
}
