using Objects;
using Storage;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

[RequireComponent(typeof(LoadingAnimation))]
public class LevelSelector : MonoBehaviour
{
	[SerializeField] private GameObject _windowPrefab;
	private LoadingAnimation _loadingAnimation;

	private void Awake()
	{
		_loadingAnimation = GetComponent<LoadingAnimation>();
	}

	private bool _changing;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!collision.CompareTag(Constants.PLAYER_TAG))
			return;

		if (!_changing)
		{
			_loadingAnimation.Animate(()=>Instantiate(_windowPrefab));
			_changing = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!collision.CompareTag(Constants.PLAYER_TAG))
			return;

		_loadingAnimation.Reset();
		_changing = false;
	}
}
