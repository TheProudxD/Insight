using Objects;
using Tools;
using UnityEngine;

[RequireComponent(typeof(LoadingAnimation))]
public class ShopOpener : MonoBehaviour
{
    [SerializeField] private GameObject _shopCanvas;
    private LoadingAnimation _loadingAnimation;

    private void Awake() =>
        _loadingAnimation = GetComponent<LoadingAnimation>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InsightUtils.IsItPlayer(collision) == false)
            return;

        _loadingAnimation.Animate(() => _shopCanvas.SetActive(true));
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (InsightUtils.IsItPlayer(collision) == false)
            return;

        _loadingAnimation.Reset();
    }
}