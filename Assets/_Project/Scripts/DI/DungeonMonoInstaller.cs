using UI;
using Zenject;

public class DungeonMonoInstaller : MonoInstaller
{
    [Inject] private Hud _hud;
    
    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromInstance(_hud.Joystick);
    }
}
