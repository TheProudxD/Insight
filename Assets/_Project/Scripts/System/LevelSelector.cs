using Objects;
using Tools;
using UnityEngine;

[RequireComponent(typeof(LoadingAnimation))]
public class LevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject _windowPrefab;
    private LoadingAnimation _loadingAnimation;
    private bool _changing;

    private void Awake() => _loadingAnimation = GetComponent<LoadingAnimation>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (InsightUtils.IsItPlayer(collision) == false)
            return;

        if (_changing) 
            return;
        
        _loadingAnimation.Animate(() => Instantiate(_windowPrefab));
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