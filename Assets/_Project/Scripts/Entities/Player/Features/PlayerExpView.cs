using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerExpView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _experienceAmountText;
        [SerializeField] private TextMeshProUGUI _levelText;
        
        private float _maxExpForLevel;

        public void Initialize(float maxExpForLevel, int currentExpAmount, int currentLevel)
        {
            _maxExpForLevel = maxExpForLevel;

            DisplayExp(currentExpAmount);
            DisplayLevel(currentLevel);
        }

        public void DisplayExp(int currentExp) => _experienceAmountText.SetText($"{currentExp}/{_maxExpForLevel}");
        
        public void DisplayLevel(int level) => _levelText.SetText(level.ToString());
    }
}