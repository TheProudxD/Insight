using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Zenject;

public class DungeonInstaller : MonoInstaller
{
    [Inject] private Hud _hud;
    
    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromInstance(_hud.Joystick);
    }
}
