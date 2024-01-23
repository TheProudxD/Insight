using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    [SerializeField] private Inventory _playerInventory;
    public override void InstallBindings()
    {
        Container.Bind<Inventory>().FromInstance(_playerInventory);
    }
}