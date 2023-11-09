using UnityEngine;
using Zenject;

namespace UI
{
    [RequireComponent(typeof(Canvas))]
    public class LobbyMenuUI : MonoBehaviour
    {
        [Inject]
        private void Construct(Camera uiCamera) => GetComponent<Canvas>().worldCamera = uiCamera;
    }
}