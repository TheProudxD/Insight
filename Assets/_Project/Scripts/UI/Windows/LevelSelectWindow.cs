using Managers;
using Storage;
using UnityEngine;
using Zenject;

namespace UI
{
    public class LevelSelectWindow : CommonWindow
    {
        [Inject] private SceneManager _sceneManager;
        [Inject] private WindowManager _windowManager;
        
        [field: SerializeField] public Sprite Star { get; private set; }
        [field: SerializeField] public Sprite PlaceholderStar { get; private set; }
    }
}