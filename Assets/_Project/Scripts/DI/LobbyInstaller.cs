using Storage;
using TMPro;
using UnityEngine;
using Zenject;

public class LobbyInstaller : MonoInstaller
{
	[SerializeField] private TextMeshProUGUI _levelPassedText;

	public override void InstallBindings()
	{
		
	}

    public void Awake()
	{
        _levelPassedText.SetText(((Scenes)Container.Resolve<SceneManager>().CurrentLevel).ToString());
	}
}