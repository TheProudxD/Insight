using Managers;
using Objects.Powerups;
using Player;
using Storage;
using StorageService;
using ResourceService;
using UI;
using UI.Shop;
using UI.Shop.Data;
using UnityEngine;
using Zenject;

public class ProjectMonoInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        GoogleSheetLoading();
        System();
        Loading();
        Data();
        Storage();
        Shop();
        Player();
    }

    private void System()
    {
        Container.Bind<WindowManager>().AsSingle();
        Container.Bind<AssetManager>().AsSingle().Lazy();
        
        var tryAmount = 3;
        var waitingDelay = 3000;
        Container
            .Bind<ConnectionManager>()
            .AsSingle()
            .WithArguments(tryAmount, waitingDelay);
    }

    private void GoogleSheetLoading()
    {
        Container
            .Bind<GoogleSheetLoader>()
            .AsSingle()
            .WithArguments("1b5Ak77i6ubJFIcFagXtlwf2mwrYZrXJ3qOPp5c85NgQ", false);
    }

    private void Loading()
    {
        Container.BindInterfacesTo<ConnectionChecker>().AsSingle();
        Container.BindInterfacesTo<GoogleSheetsDownloader>().AsSingle();
        Container.BindInterfacesTo<GameSceneLoader>().AsSingle();
        Container.BindInterfacesTo<DataLoader>().AsSingle();
        Container.Bind<LoadingScreenLoader>().AsSingle();
    }

    private void Data()
    {
        Container.Bind<DataManager>().AsSingle();
    }

    private void Shop()
    {
        Container.Bind<ShopData>().AsSingle();
        Container.Bind<OpenedSkinsChecker>().ToSelf().AsSingle();
        Container.Bind<SelectedSkinsChecker>().ToSelf().AsSingle();
        Container.Bind<SkinSelector>().ToSelf().AsSingle();
        Container.Bind<SkinUnlocker>().ToSelf().AsSingle();
    }

    private void Storage()
    {
        Container.Bind<string>().WithId("Server").FromInstance("http://game.ispu.ru/insight");
        Container.BindInterfacesTo<ServerJSONStorageService>().AsSingle();
        Container.BindInterfacesTo<ServerStorageService>().AsSingle();
        Container.Bind<ResourceManager>().AsSingle().NonLazy();
        Container.Bind<SceneManager>().AsSingle().NonLazy();
    }

    private void Player()
    {
        Container.Bind<IInputReader>().To<KeyboardInputReader>().AsSingle().Lazy();
    }
}