using UI.Shop;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class GlobalSOInstaller : ScriptableObjectInstaller<GlobalSOInstaller>
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private ShopContent _shopContent;

    public override void InstallBindings()
    {
        Container.Bind<Inventory>().FromInstance(_playerInventory);
        Container.Bind<ShopContent>().FromInstance(_shopContent);
    }
}