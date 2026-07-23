using System;
using System.Linq;
using gishadev.gmtk.Interactions.InventoryManager.Usables;
using UnityEngine;
using VContainer;

namespace gishadev.gmtk.Interactions.InventoryManager.InventorySystems
{
    public class HotbarIS : InventorySystem
    {
        public HotbarIS(IObjectResolver objectResolver) : base(objectResolver)
        {
            Usables = new IUsable[5];
            PublishUpdateEvent();
        }

        public override void AddUsable(InventoryItemDataSO inventoryItemData, Transform parent)
        {
            if (Usables.All(x => x != null))
                return;

            IUsable usable = CreateUsable(inventoryItemData, parent);
            var index = Array.FindIndex(Usables, x => x == null);
            Usables[index] = usable;
            base.AddUsable(inventoryItemData, parent);
        }
    }
}