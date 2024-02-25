using ResourceService;
using UI.Shop;
using UnityEngine;
using Zenject;

public class ShopBootstrap : MonoInstaller
{
    [SerializeField] private Canvas _shopCanvas;
    [SerializeField] private Shop _shop;
    [SerializeField] private WalletView _walletView;

    [Inject] private ResourceManager _resourceManager;

    public override void InstallBindings()
    {
        Container.Bind<OpenedSkinsChecker>().ToSelf().AsSingle();
        Container.Bind<SelectedSkinsChecker>().ToSelf().AsSingle();
        Container.Bind<SkinSelector>().ToSelf().AsSingle();
        Container.Bind<SkinUnlocker>().ToSelf().AsSingle();
    }

    public void Awake()
    {
        _shopCanvas.gameObject.SetActive(true);
        InitializeWallet();
        InitializeShop();
        _shopCanvas.gameObject.SetActive(false);
    }

    private void InitializeWallet()
    {
        _walletView.Initialize(_resourceManager.GetResourceValue(ResourceType.SoftCurrency),
            _resourceManager.GetResourceValue(ResourceType.HardCurrency));
    }

    private void InitializeShop() => _shop.Initialize();
}