using Objects;
using Tools;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class DialogManager : Interactable
    {
        [Inject] private WindowManager _windowManager;
        [Inject] private DialogAudioPlayer _dialogAudioPlayer;
        [SerializeField] private string _dialog;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;

            var dialogBox = _windowManager.ShowDialogBox();
            dialogBox.Text.SetText(_dialog);
            _dialogAudioPlayer.Play();
            PlayerInRange = true;
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;

            PlayerInRange = false;
            _dialogAudioPlayer.Stop();
            _windowManager.CloseDialogBox();
        }
    }
}