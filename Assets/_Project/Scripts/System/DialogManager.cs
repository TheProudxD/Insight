using Objects;
using Tools;
using UnityEngine;

namespace Managers
{
    public class DialogManager : Interactable
    {
        [SerializeField] private string _dialog;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;
            
            var dialogBox = WindowManager.ShowDialogBox();
            dialogBox.Text.SetText(_dialog);
            PlayerInRange = true;
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;

            PlayerInRange = false;
            WindowManager.CloseDialogBox();
        }
    }
}