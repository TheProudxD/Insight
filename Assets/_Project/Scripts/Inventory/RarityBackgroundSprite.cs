using UnityEngine;

[CreateAssetMenu(menuName = "Create RarityBackgroundSprite", fileName = "RarityBackgroundSprite", order = 0)]
public class RarityBackgroundSprite: ScriptableObject
{
    [field: SerializeField] public Sprite GoldSprite { get; private set; }
    [field: SerializeField] public Sprite RedSprite { get; private set; }
    [field: SerializeField] public Sprite PurpleSprite { get; private set; }
    [field: SerializeField] public Sprite GreenSprite { get; private set; }
    [field: SerializeField] public Sprite GraySprite { get; private set; }
}