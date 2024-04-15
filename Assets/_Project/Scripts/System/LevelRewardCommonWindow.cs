using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Storage
{
	public class LevelRewardCommonWindow: CommonWindow
    {
        [SerializeField] private GameObject _resultWindow;

        [SerializeField] private Sprite _starEmpty, _starFilled;
        [SerializeField] private Image[] _stars;
        [SerializeField] private GameObject[] _starsParticles;
        
        [SerializeField] private TextMeshProUGUI _softCurrencyAmountText;
        [SerializeField] private TextMeshProUGUI _hardCurrencyAmountText;
        [SerializeField] private GameObject _chests;

        private void Awake()
        {
            _resultWindow.SetActive(false);
            if (_stars.Length!=3)
                Debug.LogError("not equal to 3 stars in result window");
        }

        public void DisplayReward()
        {
            var softCurrencyEarned = CountSoftCurrencyEarned();
            _softCurrencyAmountText.SetText(softCurrencyEarned.ToString());
            var hardCurrencyEarned = CountHardCurrencyEarned();
            _hardCurrencyAmountText.SetText(hardCurrencyEarned.ToString());
            
            var score = CountScore();
            DisplayStars(2);
            _resultWindow.SetActive(true);
        }

        private int CountSoftCurrencyEarned()
        {
            return default;
        } 
        
        private int CountHardCurrencyEarned()
        {
            return default;
        }

        private float CountScore()
        {
            return default;
        }

        private void DisplayStars(int amount)
        {
            for (var i = 0; i < _stars.Length; i++)
            {
                var isEnough = i < amount;
                _stars[i].sprite = isEnough ? _starFilled : _starEmpty;
                _starsParticles[i].SetActive(isEnough);
            }
        }
	}
}