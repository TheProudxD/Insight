using Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelSelectPopup : MonoBehaviour
    {
        [field: SerializeField] public Button StartLevelButton { get; set; }
        [field: SerializeField] public Button SkipLevelButton { get; set; }
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _titleText;

        private void Awake()
        {
            _closeButton.Add(() => gameObject.SetActive(false));
        }

        private void SetLevelTitle(string title) => _titleText.SetText(title);

        private void DisplayEnemies()
        {
        }

        private void DisplayRewards()
        {
        }

        public void Activate(string title)
        {
            SetLevelTitle(title);
            DisplayEnemies();
            DisplayRewards();
            gameObject.SetActive(true);
        }

        public void Deactivate() => gameObject.SetActive(false);
    }
}