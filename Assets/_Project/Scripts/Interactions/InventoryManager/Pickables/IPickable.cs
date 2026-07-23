using UnityEngine;

namespace gishadev.gmtk.Interactions.InventoryManager.Pickables
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