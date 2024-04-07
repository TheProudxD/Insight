using System;
using UnityEngine;

namespace _Project.Scripts.Inventory
{
    public class BlankSlot : MonoBehaviour
    {
        public InventorySlot Child { get; private set; }
        public int Index { get; private set; }

        public void Initialize(int index) => Index = index;

        public void UpdateChildSlot(InventorySlot child)
        {
            Child = child;
        }
    }
}