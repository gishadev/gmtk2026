using UnityEngine;
using VContainer;

namespace gishadev.walkingSimulator.Interactions.InventoryManager
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