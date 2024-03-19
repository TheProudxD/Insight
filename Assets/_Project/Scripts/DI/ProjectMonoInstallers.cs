using Managers;
using Storage;
using StorageService;
using ResourceService;
using UI.Shop.Data;
using Zenject;

public class ProjectMonoInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        System();
        GoogleSheetLoading();
        Loading();
        Data();
        Storage();
        Shop();
    }

    private void System()
    {
        Container.Bind<WindowManager>().AsSingle();
        Container.Bind<AssetManager>().AsSingle();
        Container.Bind<ConnectionManager>().FromInstance(new ConnectionManager(3, 3000));
    }

    private void GoogleSheetLoading()
    {
        Container.Bind<string>().WithId("Google Sheets").FromInstance("1b5Ak77i6ubJFIcFagXtlwf2mwrYZrXJ3qOPp5c85NgQ");
        Container.Bind<bool>().WithId("Debug").FromInstance(false);
        Container.Bind<GoogleSheetLoader>().ToSelf().AsSingle();
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
        Container.Bind<string>().WithId("Server").FromInstance("http://game.ispu.ru/insight");
        Container.BindInterfacesTo<ServerJSONStorageService>().AsSingle();
        Container.BindInterfacesTo<ServerStorageService>().AsSingle();
        Container.Bind<ResourceManager>().AsSingle().NonLazy();
        Container.Bind<LevelManager>().AsSingle().Lazy();
    }
}