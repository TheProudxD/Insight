using Tools;
using UnityEngine;

namespace Objects
{
    public class GateOpener : MonoBehaviour
    {
        [SerializeField] private GameObject _closedGate;
        [SerializeField] private GameObject _openedGate;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (InsightUtils.IsItPlayer(collision) == false)
                return;
            _closedGate.SetActive(false);
            _openedGate.SetActive(true);
        }
    }
}