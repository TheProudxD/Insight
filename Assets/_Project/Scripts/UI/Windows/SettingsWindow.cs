using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
    public class SettingsWindow : CommonWindow
    {
        [SerializeField] private Button _applyButton;
        
        [Header("Language")] 
        [SerializeField] private Dropdown _languageChangeDropdown;
        [SerializeField] private LanguageChanger _languageChanger;

        [Header("Tutorial")] 
        [SerializeField] private Button _tutorialEnableButton;
        [SerializeField] private Button _tutorialDisableButton;

        [Header("Quality")] 
        [SerializeField] private Toggle _lowQualityToggle;
        [SerializeField] private Toggle _mediumQualityToggle;
        [SerializeField] private Toggle _highQualityToggle;

        [Header("Audio")] 
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private Slider _soundVolume;
        [SerializeField] private Slider _musicVolume;
        
        [Header("Other")] 
        [SerializeField] private Toggle _hotShopDealsToggle;
        [SerializeField] private Toggle _limitedTimeEventsToggle;

        protected override void OnEnable()
        {
            base.OnEnable();

            _applyButton.onClick.AddListener(Close);
            
            _languageChangeDropdown.onValueChanged.AddListener(ChangeLanguage);

            _tutorialEnableButton.onClick.AddListener(EnableTutorial);
            _tutorialDisableButton.onClick.AddListener(DisableTutorial);

            _lowQualityToggle.onValueChanged.AddListener(ToggleLowQuality);
            _mediumQualityToggle.onValueChanged.AddListener(ToggleMediumQuality);
            _highQualityToggle.onValueChanged.AddListener(ToggleHighQuality);

            _soundVolume.onValueChanged.AddListener(ChangeSoundVolume);
            _musicVolume.onValueChanged.AddListener(ChangeMusicVolume);

            _hotShopDealsToggle.onValueChanged.AddListener(ChangeHotShopDealsToggle);
            _limitedTimeEventsToggle.onValueChanged.AddListener(ChangeLimitedTimeEventsToggle);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _applyButton.onClick.RemoveListener(Close);
            
            _languageChangeDropdown.onValueChanged.RemoveListener(ChangeLanguage);

            _tutorialEnableButton.onClick.RemoveListener(EnableTutorial);
            _tutorialDisableButton.onClick.RemoveListener(DisableTutorial);

            _lowQualityToggle.onValueChanged.RemoveListener(ToggleLowQuality);
            _mediumQualityToggle.onValueChanged.RemoveListener(ToggleMediumQuality);
            _highQualityToggle.onValueChanged.RemoveListener(ToggleHighQuality);

            _soundVolume.onValueChanged.RemoveListener(ChangeSoundVolume);
            _musicVolume.onValueChanged.RemoveListener(ChangeMusicVolume);

            _hotShopDealsToggle.onValueChanged.RemoveListener(ChangeHotShopDealsToggle);
            _limitedTimeEventsToggle.onValueChanged.RemoveListener(ChangeLimitedTimeEventsToggle);
        }

        private void ChangeLanguage(int langID)
        {
            _languageChanger.SwitchLanguage(langID);
            AudioPlayer.PlayToggleSound();
        }

        private void EnableTutorial()
        {
            _tutorialEnableButton.gameObject.SetActive(false);
            _tutorialDisableButton.gameObject.SetActive(true);
            AudioPlayer.PlayButtonSound();
        }

        private void DisableTutorial()
        {
            _tutorialEnableButton.gameObject.SetActive(true);
            _tutorialDisableButton.gameObject.SetActive(false);
            AudioPlayer.PlayButtonSound();
        }

        private void ToggleLowQuality(bool value) => ChangeQuality(0);

        private void ToggleMediumQuality(bool value) => ChangeQuality(1);

        private void ToggleHighQuality(bool value) => ChangeQuality(2);

        private void ChangeSoundVolume(float sliderValue) => _audioMixer.SetFloat("Sound", sliderValue);
       
        private void ChangeMusicVolume(float sliderValue) => _audioMixer.SetFloat("Music", sliderValue);
        
        private void ChangeHotShopDealsToggle(bool arg0)
        {
            AudioPlayer.PlayToggleSound();
        }

        private void ChangeLimitedTimeEventsToggle(bool arg0)
        {
            AudioPlayer.PlayToggleSound();
        }

        private void ChangeQuality(int q)
        {
            QualitySettings.SetQualityLevel(q);
            AudioPlayer.PlayToggleSound();
        }
    }
}