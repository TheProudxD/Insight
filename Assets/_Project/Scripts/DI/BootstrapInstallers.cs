using System;
using System.Linq;
using Objects.Powerups;
using Player;
using ResourceService;
using Storage;
using UI;
using UI.Loading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BootstrapInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        var mainCamera = FindObjectOfType<Camera>();
        var hud = FindObjectOfType<Hud>();
        var playerHealth = FindObjectOfType<PlayerHealth>();
        var playerAttacking = FindObjectOfType<PlayerAttacking>();
        var playerMana = FindObjectOfType<PlayerMana>();
        var playerAbilitySystem = FindObjectOfType<PlayerAbilitySystem>();
        var joystick = FindObjectOfType<Joystick>();
        var loadingImage = FindFirstObjectByType<LoadingImage>();

        var projectContext = Container.ParentContainers.First();

        projectContext.Bind<Camera>().FromInstance(mainCamera).AsSingle().NonLazy();
        projectContext.Bind<Image>().WithId("loading image").FromInstance(loadingImage.Image).AsSingle().NonLazy();
        projectContext.Bind<Hud>().FromInstance(hud).AsSingle().NonLazy();
        projectContext.Bind<Canvas>().FromInstance(hud.GetComponent<Canvas>()).AsSingle().NonLazy();
        projectContext.Bind<Animator>().WithId("fade animator").FromInstance(FindObjectsOfType<Animator>().First(x=>x.name.ToLower().Contains("fade"))).AsSingle().NonLazy();
        projectContext.Bind<Joystick>().FromInstance(joystick).NonLazy();

        projectContext.Bind<PlayerAttacking>().FromInstance(playerAttacking).AsSingle().NonLazy();
        projectContext.Bind<PlayerHealth>().FromInstance(playerHealth).AsSingle().NonLazy();
        projectContext.Bind<PlayerMana>().FromInstance(playerMana).NonLazy();
        projectContext.Bind<PlayerAbilitySystem>().FromInstance(playerAbilitySystem).NonLazy();

        projectContext.BindFactory<Powerup, Vector3, Powerup, PowerupFactory>().ToSelf().NonLazy();
        
        var reactions = Resources.LoadAll<ItemReaction>("Item reactions");
        foreach (var reaction in reactions)
        {
            projectContext.Bind().ToSelf().FromInstance(reaction).AsSingle();
        }

        projectContext.Resolve<SceneManager>().LoadScene(Scene.Menu);
    }
}