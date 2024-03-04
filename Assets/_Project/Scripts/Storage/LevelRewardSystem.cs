using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Storage
{
	public class LevelRewardSystem: MonoBehaviour
    {
        [SerializeField] private GameObject _resultWindow;

        [SerializeField] private TextMeshProUGUI _softCurrencyAmountText;
        [SerializeField] private TextMeshProUGUI _hardCurrencyAmountText;
        [SerializeField] private GameObject _chests;

        public void GetReward()
		{
            SceneManager.LoadScene("Lobby");
            _resultWindow.SetActive(true);
        }
	}
}