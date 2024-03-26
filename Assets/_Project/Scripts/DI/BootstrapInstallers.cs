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
        var levelRewardSystem = FindObjectOfType<LevelRewardWindow>();
        var mainCamera = FindObjectOfType<Camera>();
        var hud = FindObjectOfType<Hud>();
        var playerHealth = FindObjectOfType<PlayerHealth>();
        
        ProjectContext.Instance.Container.Bind<PlayerHealth>().FromInstance(playerHealth).AsSingle();
        ProjectContext.Instance.Container.Bind<Camera>().FromInstance(mainCamera).AsSingle();
        ProjectContext.Instance.Container.Bind<Hud>().FromInstance(hud).AsSingle();
        ProjectContext.Instance.Container.Bind<Canvas>().FromInstance(hud.GetComponent<Canvas>()).AsSingle().Lazy();
        ProjectContext.Instance.Container.Bind<LevelRewardWindow>().FromInstance(levelRewardSystem);
        ProjectContext.Instance.Container.BindFactory<Powerup, Vector3, Powerup, PowerupFactory>().ToSelf();

        var reactions = Resources.LoadAll<ItemReaction>("Item reactions");
        foreach (var reaction in reactions)
        {
            ProjectContext.Instance.Container.Bind().ToSelf().FromInstance(reaction).AsSingle();
        }
    }
}