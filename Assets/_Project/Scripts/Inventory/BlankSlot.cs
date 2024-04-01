using System;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class BlankSlot : MonoBehaviour
    {
        public InventorySlot Child { get; private set; }

        public void UpdateChildSlot(InventorySlot child)
        {
            Child = child;
        }
    }
}