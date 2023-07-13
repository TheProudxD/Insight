using UnityEngine;

[CreateAssetMenu(menuName = "Create Item", fileName = "Item", order = 0)]
public class Item : ScriptableObject
{
   public Sprite ItemSprite;
   public string ItemDescription;
   public bool IsKey;
}
