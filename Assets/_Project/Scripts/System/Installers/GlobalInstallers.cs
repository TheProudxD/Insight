using Game.Scripts.Storage;
using Managers;
using StorageService;
using ResourceService;
using UI;
using UnityEngine;
using Zenject;

public class GlobalInstallers : MonoInstaller
{
    [SerializeField] private Hud _hudCanvasPrefab;

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
        var hud = Instantiate(_hudCanvasPrefab);

        DontDestroyOnLoad(camera);
        DontDestroyOnLoad(hud);

        Container.Bind<Hud>().FromInstance(hud).AsSingle();
        Container.Bind<Camera>().FromInstance(camera).AsSingle();
        Container.Bind<WindowManager>().AsSingle();
        Container.Bind<Canvas>().FromInstance(hud.GetComponent<Canvas>()).AsSingle().Lazy();
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
        Container.Bind<DataManager>().AsSingle();
        Container.Bind<Wallet>().ToSelf().AsSingle();
    }

    private void Storage()
    {
        Container.Bind<string>().FromInstance("http://game.ispu.ru/insight");
        Container.BindInterfacesTo<ServerJSONStorageService>().AsSingle();
        Container.BindInterfacesTo<ServerStorageService>().AsSingle();
        Container.Bind<ResourceManager>().ToSelf().AsSingle();
        Container.Bind<LevelManager>().ToSelf().AsSingle();
    }
}