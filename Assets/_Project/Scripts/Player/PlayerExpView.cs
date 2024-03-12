using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerExpView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _experienceAmountText;
        [SerializeField] private TextMeshProUGUI _levelText;
        private float _maxExpForLevel;

        public void Initialize(float maxExpForLevel)
        {
            _maxExpForLevel = maxExpForLevel;

            DisplayExp(10);
            DisplayLevel(1);
        }

        public void DisplayExp(int currentExp) => _experienceAmountText.SetText($"{currentExp}/{_maxExpForLevel}");
        
        public void DisplayLevel(int level) => _levelText.SetText(level.ToString());
    }
}