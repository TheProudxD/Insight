using System;
using System.Linq;
using System.Threading.Tasks;
using Objects.Powerups;
using Player;
using Storage;
using UI;
using UnityEngine;
using Zenject;

public class BootstrapInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        var mainCamera = FindObjectOfType<Camera>();
        var hud = FindObjectOfType<Hud>();
        var playerHealth = FindObjectOfType<PlayerHealth>();

        var projectContext = Container.ParentContainers.First();
        projectContext.Bind<PlayerHealth>().FromInstance(playerHealth).AsSingle().NonLazy();
        projectContext.Bind<Camera>().FromInstance(mainCamera).AsSingle().NonLazy();
        projectContext.Bind<Hud>().FromInstance(hud).AsSingle().NonLazy();
        projectContext.Bind<Canvas>().FromInstance(hud.GetComponent<Canvas>()).AsSingle().NonLazy();
        projectContext.Bind<Joystick>().FromInstance(hud.Joystick);
        projectContext.BindFactory<Powerup, Vector3, Powerup, PowerupFactory>().ToSelf().NonLazy();

        var reactions = Resources.LoadAll<ItemReaction>("Item reactions");
        foreach (var reaction in reactions)
        {
            projectContext.Bind().ToSelf().FromInstance(reaction).AsSingle();
        }
        
        projectContext.Resolve<SceneManager>().LoadScene(Scenes.Menu);
    }
}