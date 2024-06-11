using System.Globalization;
using Storage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelSelectPopup : MonoBehaviour
    {
        [field: SerializeField] public Button StartLevelButton { get; set; }
        [field: SerializeField] public Button SkipLevelButton { get; set; }

        [SerializeField] private TextMeshProUGUI _titleText;

        [SerializeField] private TextMeshProUGUI _softCurrencyText;
        [SerializeField] private TextMeshProUGUI _hardCurrencyText;
        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private RewardByLevel _rewardByLevel;

        private void SetLevelTitle(string title) => _titleText.SetText(title);

        private void DisplayRewards(Scene displayedScene)
        {
            var reward = _rewardByLevel.Get[displayedScene];

            TryToDisplayReward(reward.SoftCurrencyAmount, _softCurrencyText);
            TryToDisplayReward(reward.HardCurrencyAmount, _hardCurrencyText);
            TryToDisplayReward(reward.EnergyAmount, _energyText);
        }

        private void TryToDisplayReward(float reward, TextMeshProUGUI text)
        {
            if (reward > 0)
            {
                text.transform.parent.gameObject.SetActive(true);
                text.SetText(reward.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                text.transform.parent.gameObject.SetActive(false);
            }
        }

        public void Activate(Scene displayedScene)
        {
            SetLevelTitle(displayedScene.ToString());
            DisplayRewards(displayedScene);
            gameObject.SetActive(true);
        }

        public void Deactivate() => gameObject.SetActive(false);
    }
}