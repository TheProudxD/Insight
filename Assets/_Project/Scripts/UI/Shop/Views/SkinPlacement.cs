using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinPlacement : MonoBehaviour
{
    [SerializeField] private Image _placement;
    [SerializeField] private TextMeshProUGUI _title;
    [SerializeField] private TextMeshProUGUI _description;

    public void SetGameModel(Sprite sprite, string title, string description)
    {
        _placement.sprite = sprite;
        _title.SetText(title);
        _description.SetText(description);
    }
}