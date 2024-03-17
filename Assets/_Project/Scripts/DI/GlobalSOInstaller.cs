using Objects.Powerups;
using UI.Shop;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class GlobalSOInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private ShopContent _shopContent;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Heart _heartPrefab;

    public override void InstallBindings()
    {
        Container.Bind<Inventory>().FromInstance(_playerInventory).AsSingle();
        Container.Bind<ShopContent>().FromInstance(_shopContent).AsSingle();
        BindPowerups();
        BindSpecs();
    }

    private void BindPowerups()
    {
        Container.BindInterfacesAndSelfTo<Coin>().FromInstance(_coinPrefab).AsSingle();
        Container.BindInterfacesAndSelfTo<Heart>().FromInstance(_heartPrefab).AsSingle();
    }
    
    private void BindSpecs()
    {
        Container.Bind<LogEntitySpecs>().WithId("static log").FromResources(@"Entity Specs\static log");
        Container.Bind<LogEntitySpecs>().WithId("dynamic log").FromResources(@"Entity Specs\dynamic log");
        Container.Bind<LogEntitySpecs>().WithId("turret log").FromResources(@"Entity Specs\turret log");
        Container.Bind<PlayerEntitySpecs>().FromResources(@"Entity Specs\player");
        Container.Bind<HeartPowerupEntitySpecs>().FromResources(@"Entity Specs\one heart");
        Container.Bind<CoinPowerupEntitySpecs>().FromResources(@"Entity Specs\soft coin");
    }
}