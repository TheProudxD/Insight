using UnityEngine;
using UnityEngine.UI;

namespace UI.Loading
{
    [RequireComponent(typeof(Image))]
    public class LoadingImage : MonoBehaviour
    {
        [field:SerializeField] public Image Image { get; private set; }
    }
}