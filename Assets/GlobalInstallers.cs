using Assets.Game.Scripts.System;
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
        Container.BindInterfacesTo<LoadingFilesLoader>().AsSingle();
        Container.BindInterfacesTo<GameSceneLoader>().AsTransient();
        Container.Bind<LoadingScreenLoader>().AsSingle();
        ReadFiles();
    }

    private void ReadFiles()
    {
        Container.Bind<JsonToFileStorageService>().AsSingle();
        Container.Bind<string>().FromInstance(@"http://game.ispu.ru/insight");
        Container
            .BindInterfacesTo<ServerJsonToFileStorageService>()
            .AsSingle();
        Container.BindInterfacesAndSelfTo<ServerDynamicStorageService>().AsSingle();
    }
}
