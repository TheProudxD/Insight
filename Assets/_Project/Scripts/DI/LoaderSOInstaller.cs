using System.Linq;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "Create LobbySOInstaller", fileName = "LobbySOInstaller", order = 0)]
public class LobbySOInstaller : ScriptableObjectInstaller
{
    [SerializeField] private LootTable _5050chance;
    [SerializeField] private LootTable _75coin;
    [SerializeField] private LootTable _75heart;    
    [SerializeField] private LootTable _100coin;
    [SerializeField] private LootTable _100heart;
    
    public override void InstallBindings()
    {
        var projectContainer = Container.ParentContainers.First();
        
        projectContainer.BindInstance(_5050chance).WithId("50% 50%");
        projectContainer.BindInstance(_75coin).WithId("75% coin");
        projectContainer.BindInstance(_75heart).WithId("75% heart");
        projectContainer.BindInstance(_100coin).WithId("100% coin");
        projectContainer.BindInstance(_100heart).WithId("100% heart");
    }
}