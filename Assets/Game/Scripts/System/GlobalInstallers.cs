using Assets.Game.Scripts.System;
using StorageService;
using ResourceService;
using UnityEngine;
using Zenject;

public class GlobalInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        var camera = FindObjectOfType<Camera>();
        DontDestroyOnLoad(camera);
        Container.Bind<Camera>().FromInstance(camera).AsSingle();
        Container.Bind<DataManager>().ToSelf().AsSingle();
        Loading();
        Container.Bind<ResourceManager>().AsSingle();
        Storage();
    }

    private void Loading()
    {
        Container.BindInterfacesTo<DataLoader>().AsSingle();
        Container.BindInterfacesTo<GameSceneLoader>().AsTransient();
        Container.Bind<LoadingScreenLoader>().AsSingle();
    }
    
    private void Storage()
    {
        Container.Bind<string>().FromInstance($"http://game.ispu.ru/insight");
        Container.BindInterfacesTo<LocalJSONStorageService>().AsSingle();
        //Container.BindInterfacesTo<ServerJSONStorageService>().AsSingle();
        Container.BindInterfacesTo<DatabaseTransferService>().AsSingle();
    }
}