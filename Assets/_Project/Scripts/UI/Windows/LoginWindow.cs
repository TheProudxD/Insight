using System;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoginWindow : CommonWindow
    {
        [SerializeField] private InputField _nicknameInput;
        [SerializeField] private Button _applyButton;

        public event Action<string> NicknameWasRead;

        private void ReadUserNickname() => NicknameWasRead?.Invoke(_nicknameInput.text);

        protected override void OnEnable()
        {
            base.OnEnable();
            _applyButton.Add(ReadUserNickname);
            _applyButton.Add(AudioPlayer.PlayButtonSound);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _applyButton.Remove(ReadUserNickname);
            _applyButton.Remove(AudioPlayer.PlayButtonSound);
        }

        public override void Close()
        {
            base.Close();
            Application.Quit();
        }
    }
}