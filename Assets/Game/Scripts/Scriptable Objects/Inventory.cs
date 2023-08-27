using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create Inventory", fileName = "Item", order = 0)]
public class Inventory : ScriptableObject
{
   public Item CurrentItem;
   private List<Item> _items;
   [FormerlySerializedAs("NumberOfKey")] public int NumberOfKeys;
   public int NumberOfCoins;

   public void AddItem(Item itemToAdd)
   {
      //TODO: is enough place?

      if (itemToAdd.IsKey)
      {
         NumberOfKeys++;
      }
      else
      {
         if (!_items.Contains(itemToAdd))
         {
            _items.Add(itemToAdd);
         }
      }
   }
}
