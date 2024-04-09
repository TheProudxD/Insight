using Managers;
using TMPro;
using Tools;
using UnityEngine;
using Zenject;

namespace Objects
{
    public abstract class Interactable : MonoBehaviour
    {
        [Inject] private AssetManager _assetManager;
        [SerializeField] protected Signal Context;

        protected bool PlayerInRange;
        protected GameObject DialogBox;
        protected TextMeshProUGUI DialogUI;

        private void Awake()
        {
            DialogBox = _assetManager.GetDialogBox();
            DialogUI = DialogBox.GetComponentInChildren<TextMeshProUGUI>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;
            
            Context.Raise();
            PlayerInRange = true;
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            if (InsightUtils.IsItPlayer(other) == false)
                return;
            
            Context.Raise();
            PlayerInRange = false;
        }
    }
}