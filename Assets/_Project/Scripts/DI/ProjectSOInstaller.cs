using System;
using System.IO;
using System.Threading.Tasks;
using Objects.Powerups;
using UI.Shop;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "Create " + nameof(ProjectSOInstaller), menuName = "Installers/SOInstaller")]
public class ProjectSOInstaller : ScriptableObjectInstaller
{
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private ShopContent _shopContent;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Heart _heartPrefab;
    
    public override void InstallBindings()
    {
        Container.Bind<PlayerInventory>().FromInstance(_playerInventory).AsSingle();
        Container.Bind<ShopContent>().FromInstance(_shopContent).AsSingle();
        BindPowerups();
    }

    private void BindPowerups()
    {
        Container.BindInterfacesAndSelfTo<Coin>().FromInstance(_coinPrefab).AsSingle();
        Container.BindInterfacesAndSelfTo<Heart>().FromInstance(_heartPrefab).AsSingle();
    }
}