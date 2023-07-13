using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Create Inventory", fileName = "Item", order = 0)]
public class Inventory : ScriptableObject
{
   public Item CurrentItem;
   public List<Item> Items = new List<Item>();
   [FormerlySerializedAs("NumberOfKey")] public int NumberOfKeys;

   public void AddItem(Item itemToAdd)
   {
      //TODO: is enough place?

      if (itemToAdd.IsKey)
      {
         NumberOfKeys++;
      }
      else
      {
         if (!Items.Contains(itemToAdd))
         {
            Items.Add(itemToAdd);
         }
      }
   }
}
