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
        Container.Bind<EntitySpecs>().WithId("log").FromResources(@"Entity Specs\log").AsSingle();
    }
}