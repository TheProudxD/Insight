using Tools;
using UI.Shop;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class GlobalSOInstaller : ScriptableObjectInstaller
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private ShopContent _shopContent;

    public override void InstallBindings()
    {
        Container.Bind<Inventory>().FromInstance(_playerInventory).AsSingle();
        Container.Bind<ShopContent>().FromInstance(_shopContent).AsSingle();
        Container.Bind<LogEntitySpecs>().WithId("static log").FromResources(@"Entity Specs\static log");
        Container.Bind<LogEntitySpecs>().WithId("dynamic log").FromResources(@"Entity Specs\dynamic log");
        Container.Bind<LogEntitySpecs>().WithId("turret log").FromResources(@"Entity Specs\turret log");
    }
}