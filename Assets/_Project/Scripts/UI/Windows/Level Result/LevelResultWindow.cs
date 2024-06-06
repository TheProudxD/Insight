using System;
using UI;
using UnityEngine;
using Zenject;

namespace Storage
{
    public class LevelResultWindow : CommonWindow
    {
        [Inject] private LevelResultAudioPlayer _audioPlayer;
        
        [SerializeField] private SuccessfulResultWindow _successfulResultWindow;
        [SerializeField] private FailedResultWindow _failedResultWindow;

        public void Display(LevelResultType levelResultType)
        {
            switch (levelResultType)
            {
                case LevelResultType.Successful:
                    _successfulResultWindow.gameObject.SetActive(true);
                    _failedResultWindow.gameObject.SetActive(false);
                    _audioPlayer.PlayLevelCompleted();
                    break;
                case LevelResultType.Failed:
                    _successfulResultWindow.gameObject.SetActive(false);
                    _failedResultWindow.gameObject.SetActive(true);
                    _audioPlayer.PlayLevelFailed();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(levelResultType), levelResultType, null);
            }
        }
    }
}