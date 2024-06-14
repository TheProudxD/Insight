using System.Linq;
using System.Reflection;
using Objects.Powerups;
using Player;
using Storage;
using UI;
using UI.Loading;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoaderInstaller : MonoInstaller
{
    [SerializeField] private BGMAudioPlayer _bgmAudioPlayer;
    [SerializeField] private CharacterAudioPlayer _characterAudioPlayer;
    [SerializeField] private DialogAudioPlayer _dialogAudioPlayer;
    [SerializeField] private HitAudioPlayer _hitAudioPlayer;
    [SerializeField] private UIAudioPlayer _uiAudioPlayer;
    [SerializeField] private AbilityAudioPlayer _abilityAudioPlayer;
    [SerializeField] private LevelResultAudioPlayer _levelResultAudioPlayer;

    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Hud _hud;
    [SerializeField] private Animator _fadeAnimator;
    
    private DiContainer _projectContainer;

    public override void InstallBindings()
    {
        var joystick = FindObjectOfType<Joystick>();
        var loadingImage = FindFirstObjectByType<LoadingImage>();
        _projectContainer = Container.ParentContainers.First();

        _projectContainer.Bind<Camera>().FromInstance(_mainCamera).AsSingle().NonLazy();
        _projectContainer.Bind<Image>().WithId("loading image").FromInstance(loadingImage.Image).AsSingle().NonLazy();
        _projectContainer.Bind<Hud>().FromInstance(_hud).AsSingle().NonLazy();
        _projectContainer.Bind<Canvas>().FromInstance(_hud.GetComponent<Canvas>()).AsSingle().NonLazy();
        _projectContainer.Bind<Animator>().WithId("fade animator").FromInstance(_fadeAnimator).AsSingle().NonLazy();
        _projectContainer.Bind<Joystick>().FromInstance(joystick).NonLazy();
        
        _projectContainer.BindInterfacesAndSelfTo<RewardsByLevelManager>().AsSingle();
        BindAbilitySystem();
        BindAbilityReactions();
        BindPowerupFactory();
        BindAudioPlayers();
    }

    private void BindAudioPlayers()
    {
        _projectContainer.Bind<BGMAudioPlayer>().FromInstance(_bgmAudioPlayer);
        _projectContainer.Bind<CharacterAudioPlayer>().FromInstance(_characterAudioPlayer);
        _projectContainer.Bind<DialogAudioPlayer>().FromInstance(_dialogAudioPlayer);
        _projectContainer.Bind<HitAudioPlayer>().FromInstance(_hitAudioPlayer);
        _projectContainer.Bind<UIAudioPlayer>().FromInstance(_uiAudioPlayer);
        _projectContainer.Bind<AbilityAudioPlayer>().FromInstance(_abilityAudioPlayer);
        _projectContainer.Bind<LevelResultAudioPlayer>().FromInstance(_levelResultAudioPlayer);
    }

    private void BindAbilitySystem()
    {
        var playerHealth = FindObjectOfType<PlayerHealth>();
        var playerAttacking = FindObjectOfType<PlayerAttacking>();
        var playerMana = FindObjectOfType<PlayerMana>();
        var playerAbilitySystem = FindObjectOfType<PlayerAbilitySystem>();

        _projectContainer.Bind<PlayerAttacking>().FromInstance(playerAttacking).AsSingle().NonLazy();
        _projectContainer.Bind<PlayerHealth>().FromInstance(playerHealth).AsSingle().NonLazy();
        _projectContainer.Bind<PlayerMana>().FromInstance(playerMana).NonLazy();
        _projectContainer.Bind<PlayerAbilitySystem>().FromInstance(playerAbilitySystem).NonLazy();
    }

    private void BindAbilityReactions()
    {
        var reactions = Resources.LoadAll<ItemReaction>("Item reactions");
        foreach (var reaction in reactions) 
            _projectContainer.Bind().ToSelf().FromInstance(reaction).AsSingle();
    }

    private void BindPowerupFactory() =>
        _projectContainer.BindFactory<Powerup, Vector3, Powerup, PowerupFactory>().ToSelf().NonLazy();

    public override void Start() => _projectContainer.Resolve<SceneManager>().LoadScene(Scene.Menu);
}