using System.Linq;
using Storage;
using StorageService;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LevelSelectWindow : CommonWindow
    {
        [Inject] private DataManager _dataManager;
        [Inject] private SceneManager _sceneManager;

        [SerializeField] private Level[] _levels;
        [SerializeField] private TextMeshProUGUI _starsAmount;
        [SerializeField] private TextMeshProUGUI _chapter;

        [field: SerializeField] public Sprite Star { get; private set; }
        [field: SerializeField] public Sprite PlaceholderStar { get; private set; }

        public void Start()
        {
            var isOpen = _sceneManager.MaxPassedLevel;
            _levels.Where(x => (int)x.Scene <= _sceneManager.MaxPassedLevel)
                .OrderByDescending(s => (int)s.Scene)
                .First()
                .OpenPopupButton.onClick.Invoke();

            float currentStarsAmount = 6, maxStarsAmount = 18, chapter = 1;
            _starsAmount.SetText($"{currentStarsAmount} / {maxStarsAmount}");
            _chapter.SetText($"Chapter {chapter}");
        }
    }
}