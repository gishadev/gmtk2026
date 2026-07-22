using System;
using UnityEngine;

namespace gishadev.walkingSimulator.Interactions.InventoryManager
{
    public interface IUsable
    {
        InventoryItemDataSO InventoryItemData { get; }
        event Action Used;

        void Use();
        void Dispose();
        
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}