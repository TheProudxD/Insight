using Extensions;
using Managers;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class Level : MonoBehaviour
    {
        [Inject] private SceneManager _sceneManager;
        [Inject] private WindowManager _windowManager;

        [SerializeField] private LevelSelectWindow _levelSelectWindow;
        [SerializeField] private LevelSelectPopup _levelSelectPopup;

        [SerializeField] private Scenes _scene;
        [SerializeField] private Image[] _starImages;
        [SerializeField] private Image _lock;
        [SerializeField] private TextMeshProUGUI _requireLevelText;

        [SerializeField, Range(0, 3)] private int _starPassedAmount;

        private Button _openPopupButton;

        private void Awake()
        {
            _openPopupButton = GetComponent<Button>();
            if (_starImages.Length > 3)
                Debug.LogError(nameof(_starImages));

            InitializeOpenButton();
            InitializeUI();
        }

        private void InitializeOpenButton()
        {
            _openPopupButton.onClick.AddListener(() =>
            {
                _levelSelectPopup.gameObject.SetActive(true);
                _levelSelectPopup.Activate(_scene.ToString());
                _levelSelectPopup.StartLevelButton.RemoveAll();
                _levelSelectPopup.StartLevelButton.Add(() =>
                {
                    _windowManager.CloseLevelSelectWindow();
                    _sceneManager.LoadScene(_scene);
                });
            });
        }

        private void InitializeUI()
        {
            var isOpen = _sceneManager.MaxPassedLevel >= (int)_scene;
            if (isOpen)
            {
                for (var i = 0; i < _starImages.Length; i++)
                {
                    var starImage = _starImages[i];
                    starImage.sprite = i < _starPassedAmount
                        ? _levelSelectWindow.Star
                        : _levelSelectWindow.PlaceholderStar;

                    starImage.gameObject.SetActive(true);
                }

                _openPopupButton.interactable = true;
                _requireLevelText.gameObject.SetActive(false);
                _lock.gameObject.SetActive(false);
            }
            else
            {
                foreach (var starImage in _starImages)
                {
                    starImage.gameObject.SetActive(false);
                }

                _openPopupButton.interactable = false;

                _lock.gameObject.SetActive(true);
                _requireLevelText.gameObject.SetActive(true);
                _requireLevelText.SetText("Required level " + (int)_scene);
            }
        }
    }
}