using Managers;
using TMPro;
using UnityEngine;

namespace Objects
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected Signal Context;

        protected bool PlayerInRange;
        protected GameObject DialogBox;
        protected TextMeshProUGUI DialogUI;

        private void Awake()
        {
            if (!AssetManager.DialogAlreadySpawned)
            {
                DialogBox = AssetManager.GetDialogBoxPrefab();
                DialogUI = DialogBox.GetComponentInChildren<TextMeshProUGUI>();
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                Context.Raise();
                PlayerInRange = true;
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !other.isTrigger)
            {
                Context.Raise();
                PlayerInRange = false;
            }
        }
    }
}