using gishadev.gmtk.Interactions.InventoryManager.Usables;
using UnityEngine;
using VContainer;

namespace gishadev.gmtk.Interactions.InventoryManager.InventorySystems
{
    public class SingleSlotIS : InventorySystem
    {
        public SingleSlotIS(IObjectResolver objectResolver) : base(objectResolver)
        {
            Usables = new IUsable[1];
            PublishUpdateEvent();
        }

        public override void AddUsable(InventoryItemDataSO inventoryItemData, Transform parent)
        {
            if (Usables[0] != null)
                return;

            IUsable usable = CreateUsable(inventoryItemData, parent);
            Usables[0] = usable;
            base.AddUsable(inventoryItemData, parent);
        }
    }
}