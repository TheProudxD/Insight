using System;
using UI;
using UnityEngine;

namespace Storage
{
    public class LevelResultWindow : CommonWindow
    {
        [SerializeField] private SuccessfulResultWindow _successfulResultWindow;
        [SerializeField] private FailedResultWindow _failedResultWindow;

        public void Display(LevelResult levelResult)
        {
            switch (levelResult)
            {
                case LevelResult.Successful:
                    _successfulResultWindow.gameObject.SetActive(true);
                    _failedResultWindow.gameObject.SetActive(false);
                    break;
                case LevelResult.Failed:
                    _successfulResultWindow.gameObject.SetActive(false);
                    _failedResultWindow.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(levelResult), levelResult, null);
            }
        }
    }
}