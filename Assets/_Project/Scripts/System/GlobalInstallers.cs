using Game.Scripts.Storage;
using Managers;
using StorageService;
using ResourceService;
using UnityEngine;
using Zenject;

public class GlobalInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        System();
        Loading();
        Data();
        Storage();
    }

    private void System()
    {
        var camera = FindObjectOfType<Camera>();
        DontDestroyOnLoad(camera);
        Container.Bind<Camera>().FromInstance(camera).AsSingle();

        Container.Bind<WindowManager>().AsSingle();
        Container.Bind<AssetManager>().AsSingle();
    }

    private void Loading()
    {
        Container.BindInterfacesTo<DataLoader>().AsSingle();
        Container.BindInterfacesTo<GameSceneLoader>().AsTransient();
        Container.Bind<LoadingScreenLoader>().AsSingle();
    }

    private void Data()
    {
        Container.Bind<DataManager>().ToSelf().AsSingle();
        Container.Bind<ResourceManager>().AsSingle();
        Container.Bind<LevelManager>().AsSingle();
    }

    private void Storage()
    {
        Container.Bind<string>().FromInstance("http://game.ispu.ru/insight");
        Container.BindInterfacesTo<LocalJSONStorageService>().AsSingle();
        Container.BindInterfacesTo<ServerStorageService>().AsSingle();
        //Container.BindInterfacesTo<ServerJSONStorageService>().AsSingle();
    }
}