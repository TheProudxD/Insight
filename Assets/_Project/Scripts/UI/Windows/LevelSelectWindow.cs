using Storage;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI
{
	public class LevelSelectWindow : WindowCommon
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _menuButton;

		[Inject] private LevelManager _levelManager;

		private void Awake()
		{
            _closeButton.onClick.AddListener(() => Destroy(gameObject));
			//_menuButton.onClick.AddListener(() => _levelManager.LoadScene(Levels.Menu));
			_menuButton.onClick.AddListener(() => SceneManager.LoadScene((int)Levels.Menu));
		}
	}
}