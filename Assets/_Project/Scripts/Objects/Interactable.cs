using Managers;
using TMPro;
using Tools;
using UI;
using UnityEngine;
using Zenject;

namespace Objects
{
    public abstract class Interactable : MonoBehaviour
    {
        [SerializeField] protected Signal Context;

        protected bool PlayerInRange;

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