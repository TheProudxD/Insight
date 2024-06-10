using Extensions;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public class LobbyMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _VKButton;
        [SerializeField] private Button _discordButton;
        [SerializeField] private Button _leaderboardButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _newsButton;
        
        private WindowManager _windowManager;
        private UIAudioPlayer _audioPlayer;

        [Inject]
        private void Construct(Camera uiCamera, WindowManager windowManager, UIAudioPlayer audioPlayer)
        {
            GetComponent<Canvas>().worldCamera = uiCamera;
            _windowManager = windowManager;
            _audioPlayer = audioPlayer;
        }

        private void OnEnable()
        {
            _VKButton.Add(OpenVk);
            _discordButton.Add(OpenDiscord);
            
            _leaderboardButton.Add(OpenLeaderboardWindow);
            _settingsButton.Add(OpenSettingsWindow);
            _newsButton.Add(OpenNewsWindow);
        }

        private void OpenVk()
        {
            _audioPlayer.PlayButtonSound();
            Application.OpenURL("https://vk.com/callmeproud");
        }

        private void OpenDiscord()
        {
            _audioPlayer.PlayButtonSound();
            Application.OpenURL("https://vk.com/callmeproud");
        }

        private void OpenLeaderboardWindow()
        {
            _audioPlayer.PlayButtonSound();
            _windowManager.ShowLeaderboardWindow();
        }

        private void OpenSettingsWindow()
        {
            _audioPlayer.PlayButtonSound();
            _windowManager.ShowSettingsWindow();
        }

        private void OpenNewsWindow()
        {
            _audioPlayer.PlayButtonSound();
            _windowManager.ShowNewsWindow();
        }
    }
}