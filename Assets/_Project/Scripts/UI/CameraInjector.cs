using UnityEngine;
using Zenject;

namespace UI
{
    public class CameraInjector : MonoBehaviour
    {
        [Inject] private Camera _uiCamera;

        private void Awake() => 
            GetComponent<Canvas>().worldCamera = _uiCamera;
    }
}