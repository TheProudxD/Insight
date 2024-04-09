using Player;
using Zenject;

public class DungeonMonoInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<PlayerInteraction>().FromInstance(FindObjectOfType<PlayerInteraction>()); 
    }
}
