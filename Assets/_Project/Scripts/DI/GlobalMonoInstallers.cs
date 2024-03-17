using Storage;
using Managers;
using Objects.Powerups;
using StorageService;
using ResourceService;
using UI;
using UI.Shop.Data;
using UnityEngine;
using Zenject;

public class GlobalMonoInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        System();
        Loading();
        Data();
        Storage();
        Shop();
    }

    private void System()
    {
        var levelRewardSystem = FindObjectOfType<LevelRewardSystem>();
        var camera = FindObjectOfType<Camera>();
        var hud = FindObjectOfType<Hud>();

        Container.Bind<LevelRewardSystem>().FromInstance(levelRewardSystem);
        Container.Bind<Hud>().FromInstance(hud).AsSingle();
        Container.Bind<Camera>().FromInstance(camera).AsSingle();
        Container.Bind<WindowManager>().AsSingle();
        Container.Bind<Canvas>().FromInstance(hud.GetComponent<Canvas>()).AsSingle().Lazy();
        Container.Bind<AssetManager>().AsSingle();
        
        Container.BindFactory<Powerup, Vector3, Powerup, PowerupFactory>().ToSelf();
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

    private void Shop()
    {
        Container.Bind<ShopData>().AsSingle();
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