using System;
using UnityEngine;

namespace gishadev.walkingSimulator.Interactions.InventoryManager
{
    [RequireComponent(typeof(Rigidbody))]
    public class SimplePickable : MonoBehaviour, IPickable
    {
        [field: SerializeField] public InventoryItemDataSO InventoryItemData { get; private set; }
        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        public void OnPickup(Transform pickupParent)
        {
        }

    }
}