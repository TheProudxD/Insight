﻿using Extensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ExitCommonWindow : CommonWindow
    {
        [SerializeField] private Button _exitButton;

        public void OnExit(UnityAction exit)
        {
            _exitButton.Add(AudioPlayer.PlayButtonSound);
            _exitButton.Add(exit);
        }
    }
}