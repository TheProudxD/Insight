using Extensions;
using ResourceService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Storage
{
    public class SuccessfulResultWindow : ResultWindowPlaceholder
    {
        [Inject] private ResourceManager _resourceManager;

        [SerializeField] private Button _levelSelectButton;
        [SerializeField] private Sprite _starFilled;
        [SerializeField] private Image[] _stars;

        [Header("Rewards")] [SerializeField] private TextMeshProUGUI _softCurrencyAmountText;
        [SerializeField] private TextMeshProUGUI _hardCurrencyAmountText;
        [SerializeField] private TextMeshProUGUI _energyAmountText;

        [SerializeField] private RewardByLevel _rewards;

        private void Awake()
        {
            if (_stars.Length != 3)
                Debug.LogError("not equal to 3 stars in result window");
        }

        private int CountSoftCurrencyEarned() => _rewards.Get[SceneManager.CurrentScene].SoftCurrencyAmount;

        private int CountHardCurrencyEarned() => _rewards.Get[SceneManager.CurrentScene].HardCurrencyAmount;

        private int CountEnergyEarned() => _rewards.Get[SceneManager.CurrentScene].EnergyAmount;

        private void DisplayStars(int amount)
        {
            for (var i = 0; i < _stars.Length; i++)
            {
                _stars[i].sprite = _starFilled;
                var isEnough = i < amount;
                _stars[i].color = isEnough ? Color.white : Color.gray;
            }
        }

        private void OpenLevelSelectWindow()
        {
            WindowManager.ShowLevelSelectWindow();
            Close();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            DisplayReward();
            CalculateStarsAmount();
            _levelSelectButton.Add(OpenLevelSelectWindow);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _levelSelectButton.Remove(OpenLevelSelectWindow);
        }

        private void CalculateStarsAmount()
        {
            var currentScore = CountCurrentScore();
            var highScore = CountHighScore();
            var starsAmount = Mathf.RoundToInt((float)currentScore / highScore * 3);
            starsAmount = Mathf.Clamp(starsAmount, 1, 3);
            DisplayStars(starsAmount);
        }

        private void DisplayReward()
        {
            var softCurrencyEarned = CountSoftCurrencyEarned();
            _softCurrencyAmountText.SetText(softCurrencyEarned.ToString());
            var hardCurrencyEarned = CountHardCurrencyEarned();
            _hardCurrencyAmountText.SetText(hardCurrencyEarned.ToString());
            var energyEarned = CountEnergyEarned();
            _energyAmountText.SetText(energyEarned.ToString());

            SaveReward();
            return;

            void SaveReward()
            {
                _resourceManager.Add(ResourceType.SoftCurrency, softCurrencyEarned);
                _resourceManager.Add(ResourceType.HardCurrency, hardCurrencyEarned);
                _resourceManager.Add(ResourceType.Energy, energyEarned);
            }
        }
    }
}