using Managers;
using Objects.Powerups;
using Storage;
using StorageService;
using UI;
using UnityEngine;
using Zenject;

public class BootstrapInstallers : MonoInstaller
{
    public override void InstallBindings()
    {
        var levelRewardSystem = FindObjectOfType<LevelRewardSystem>();
        var camera = FindObjectOfType<Camera>();
        var hud = FindObjectOfType<Hud>();

        ProjectContext.Instance.Container.Bind<Camera>().FromInstance(camera).AsSingle();
        ProjectContext.Instance.Container.Bind<Hud>().FromInstance(hud).AsSingle();
        ProjectContext.Instance.Container.Bind<Canvas>().FromInstance(hud.GetComponent<Canvas>()).AsSingle().Lazy();
        ProjectContext.Instance.Container.Bind<LevelRewardSystem>().FromInstance(levelRewardSystem);
        ProjectContext.Instance.Container.BindFactory<Powerup, Vector3, Powerup, PowerupFactory>().ToSelf();
    }
}