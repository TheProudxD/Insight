using Managers;
using Objects;
using Tools;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(LoadingAnimation))]
public class LevelSelector : MonoBehaviour
{
    [Inject] private WindowManager _windowManager;
    
    private readonly float _loadingSpeed = 0.15f;
    private LoadingAnimation _loadingAnimation;
    private bool _changing;

    private void Awake() => _loadingAnimation = GetComponent<LoadingAnimation>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InsightUtils.IsItPlayer(collision) == false)
            return;

        if (_changing) 
            return;
        
        _loadingAnimation.Animate(() =>_windowManager.ShowLevelSelectWindow());
        _changing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (InsightUtils.IsItPlayer(collision) == false)
            return;

        _loadingAnimation.Reset();
        _changing = false;
    }
}