using Managers;
using Objects;
using Tools;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(LoadingAnimation))]
public class ShopOpener : MonoBehaviour
{
    [Inject] private WindowManager _windowManager;
    
    private LoadingAnimation _loadingAnimation;
    
    private void Awake() =>
        _loadingAnimation = GetComponent<LoadingAnimation>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InsightUtils.IsItPlayer(collision) == false)
            return;

        _loadingAnimation.Animate(() => _windowManager.ShowShopWindow());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (InsightUtils.IsItPlayer(collision) == false)
            return;

        _loadingAnimation.Reset();
    }
}