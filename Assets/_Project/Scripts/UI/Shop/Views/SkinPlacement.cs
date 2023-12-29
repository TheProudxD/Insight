using UnityEngine;
using UnityEngine.UI;

public class SkinPlacement : MonoBehaviour
{
    [SerializeField] private Image _placement;

    public void SetGameModel(Sprite sprite)
    {
        _placement.sprite = sprite;
    }
}