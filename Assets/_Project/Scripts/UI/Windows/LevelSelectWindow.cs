using StorageService;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LevelSelectWindow : CommonWindow
    {
        [Inject] private DataManager _dataManager;

        [SerializeField] private TextMeshProUGUI _starsAmount;
        [SerializeField] private TextMeshProUGUI _chapter;

        [field: SerializeField] public Sprite Star { get; private set; }
        [field: SerializeField] public Sprite PlaceholderStar { get; private set; }

        public void Awake()
        {
            float currentStarsAmount = 6, maxStarsAmount = 18, chapter = 1;
            _starsAmount.SetText($"{currentStarsAmount} / {maxStarsAmount}");
            _chapter.SetText($"Chapter {chapter}");
        }
    }
}