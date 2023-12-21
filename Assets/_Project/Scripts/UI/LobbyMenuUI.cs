using System;
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
        
        [Inject] private void Construct(Camera uiCamera) => GetComponent<Canvas>().worldCamera = uiCamera;

        private void Awake()
        {
            _VKButton.onClick.AddListener(()=>Application.OpenURL("https://vk.com/callmeproud"));
        }
    }
}