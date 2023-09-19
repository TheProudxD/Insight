using Objects;
using UnityEngine;

namespace Managers
{
    public class DialogManager : Interactable
    {
        [SerializeField] private string _dialog;

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                DialogUI.text = _dialog;
                DialogBox.SetActive(true);
                PlayerInRange = true;
            }
        }

        protected override void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                PlayerInRange = false;
                DialogBox.SetActive(false);
            }
        }
    }
}