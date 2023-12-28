using UI.Shop;
using UnityEngine;
using Zenject;

public class ShopBootstrap : MonoInstaller
{
    [SerializeField] private Shop _shop;
    [SerializeField] private WalletView _walletView;

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

    private void InitializeWallet() => _walletView.Initialize();

    private void InitializeShop() => _shop.Initialize();
}