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

        [Inject]
        private void Construct(Camera uiCamera, WindowManager windowManager)
        {
            _windowManager = windowManager;
            GetComponent<Canvas>().worldCamera = uiCamera;
        }

        private void OnEnable()
        {
            _VKButton.Add(OpenVk);
            _discordButton.Add(OpenDiscord);
            
            _leaderboardButton.Add(OpenLeaderboardWindow);
            _settingsButton.Add(OpenSettingsWindow);
            _newsButton.Add(OpenNewsWindow);
        }

        private void OpenVk() => Application.OpenURL("https://vk.com/callmeproud");

        private void OpenDiscord() => Application.OpenURL("https://vk.com/callmeproud");
        
        private void OpenLeaderboardWindow() => _windowManager.ShowLeaderboardWindow();
        
        private void OpenSettingsWindow() => _windowManager.ShowSettingsWindow();
        
        private void OpenNewsWindow() => _windowManager.ShowNewsWindow();
    }
}