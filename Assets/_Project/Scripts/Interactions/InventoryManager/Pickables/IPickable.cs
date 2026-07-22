using UnityEngine;

namespace gishadev.walkingSimulator.Interactions.InventoryManager
{
    public interface IPickable
    {
        InventoryItemDataSO InventoryItemData { get; }
        void OnPickup(Transform pickupParent);
        Rigidbody Rigidbody { get; }
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}