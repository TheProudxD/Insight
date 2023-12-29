using ResourceService;
using StorageService;
using UI.Shop;
using UnityEngine;
using Zenject;

public class ShopBootstrap : MonoInstaller
{
    [SerializeField] private Shop _shop;
    [SerializeField] private WalletView _walletView;

    [Inject] private DataManager _dataManager;

    public override void InstallBindings()
    {
        Container.Bind<OpenedSkinsChecker>().ToSelf().AsSingle();
        Container.Bind<SelectedSkinsChecker>().ToSelf().AsSingle();
        Container.Bind<SkinSelector>().ToSelf().AsSingle();
        Container.Bind<SkinUnlocker>().ToSelf().AsSingle();
    }

    public void Awake()
    {
        InitializeWallet();
        InitializeShop();
    }

    private void InitializeWallet() =>
        _walletView.Initialize(_dataManager.ResourceManager.GetResourceValue(ResourceType.SoftCurrency));

    private void InitializeShop() => _shop.Initialize();
}