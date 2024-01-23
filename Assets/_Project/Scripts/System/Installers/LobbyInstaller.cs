using Managers;
using UI;
using UnityEngine;
using Zenject;

public class LobbyInstaller : MonoInstaller
{
    [Inject] private Hud _hud;

    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromInstance(_hud.Joystick);
    }
}