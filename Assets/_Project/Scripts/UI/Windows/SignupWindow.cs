using System;
using System.Linq;
using Extensions;
using Managers;
using StorageService;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class SignupWindow : CommonWindow
    {
        [Inject] private WindowManager _windowManager;
        [Inject] private DataManager _dataManager;

        [SerializeField] private TMP_InputField _nicknameInput;
        [SerializeField] private Button _applyButton;
        [SerializeField] private Button _loginButton;
        [SerializeField] private Button _openHintButton;
        [SerializeField] private GameObject _hintObject;
        [SerializeField] private Image _rightNicknameImage;
        [SerializeField] private Sprite _greenCheck;
        [SerializeField] private Sprite _redCheck;
        
        private int _hintOpenCounter;

        private async void ReadUserNickname() => await _dataManager.SetName(_nicknameInput.text);

        protected override void OnEnable()
        {
            base.OnEnable();
            _nicknameInput.onValueChanged.AddListener(CheckEnteredNickname);
            _applyButton.Add(ReadUserNickname);
            _applyButton.Add(AudioPlayer.PlayButtonSound);
            _applyButton.Add(Close);
            _openHintButton.Add(ToggleHintForNickname);
            _loginButton.Add(OpenLoginWindow);
        }

        private void OpenLoginWindow()
        {
            Close();
            _windowManager.ShowLoginWindow();
        }

        private void ToggleHintForNickname()
        {
            _hintObject.SetActive(_hintOpenCounter % 2 == 0);
            _hintOpenCounter++;
        }

        private void CheckEnteredNickname(string nick)
        {
            var ok = nick.Length > 4 && nick.Any(char.IsLetter);

            _rightNicknameImage.sprite = ok ? _greenCheck : _redCheck;
            _applyButton.interactable = ok;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _nicknameInput.onValueChanged.RemoveListener(CheckEnteredNickname);
            _applyButton.Remove(ReadUserNickname);
            _applyButton.Remove(AudioPlayer.PlayButtonSound);
            _applyButton.Remove(Close);
            _openHintButton.Remove(ToggleHintForNickname);
            _loginButton.Remove(OpenLoginWindow);
        }
    }
}