using Extensions;
using Managers;
using ResourceService;
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
        [Inject] private ResourceManager _resourceManager;
        [Inject] private UIAudioPlayer _audioPlayer;

        [SerializeField] private LevelSelectWindow _levelSelectWindow;
        [SerializeField] private LevelSelectPopup _levelSelectPopup;

        [SerializeField] private Image[] _starImages;
        [SerializeField] private Image _lock;
        [SerializeField] private TextMeshProUGUI _requireLevelText;
        [SerializeField] private int _energyPrice;
        [SerializeField, Range(0, 3)] private int _starPassedAmount;
        
        [field: SerializeField] public Scene Scene { get; private set; }
        public Button OpenPopupButton { get; private set; }

        private void Awake()
        {
            OpenPopupButton = GetComponent<Button>();
            if (_starImages.Length > 3)
                Debug.LogError(nameof(_starImages));
        }

        private void OnEnable()
        {
            InitializeOpenButton();
            InitializeUI();
        }

        private void InitializeOpenButton()
        {
            OpenPopupButton.onClick.AddListener(() =>
            {
                _audioPlayer.PlayButtonSound();
                _levelSelectPopup.gameObject.SetActive(true);
                _levelSelectPopup.Activate(Scene);
                _levelSelectPopup.StartLevelButton.RemoveAll();
                
                if (_resourceManager.IsEnough(ResourceType.Energy, _energyPrice))
                {
                    InitializeStartLevelButton();
                }
                else
                {
                    _levelSelectPopup.StartLevelButton.interactable = false;
                }
            });
        }

        private void InitializeStartLevelButton()
        {
            _levelSelectPopup.StartLevelButton.Add(() =>
            {
                _audioPlayer.PlayButtonSound();
                _resourceManager.Spend(ResourceType.Energy, _energyPrice);
                _windowManager.CloseLevelSelectWindow();
                _sceneManager.LoadScene(Scene);
            });
        }

        private void InitializeUI()
        {
            var isOpen = _sceneManager.MaxPassedLevel >= (int)Scene;
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

                OpenPopupButton.interactable = true;
                _requireLevelText.gameObject.SetActive(false);
                _lock.gameObject.SetActive(false);
            }
            else
            {
                foreach (var starImage in _starImages)
                {
                    starImage.gameObject.SetActive(false);
                }

                OpenPopupButton.interactable = false;

                _lock.gameObject.SetActive(true);
                _requireLevelText.gameObject.SetActive(true);
                _requireLevelText.SetText("Required level " + _sceneManager.GetLevelId(Scene));
            }
        }
    }
}