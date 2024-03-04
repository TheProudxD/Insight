using Storage;
using TMPro;
using UI;
using UnityEngine;
using Zenject;

public class LobbyInstaller : MonoInstaller
{
    [Inject] private Hud _hud;

    [SerializeField] private TextMeshProUGUI _levelPassedText;

    public override void InstallBindings()
    {
        Container.Bind<Joystick>().FromInstance(_hud.Joystick);
    }

    public void Awake()
	{
        _levelPassedText.SetText(((Levels)Container.Resolve<LevelManager>().CurrentLevel).ToString());
	}
}