using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gishadev.walkingSimulator.Interactions.InventoryManager
{
    public class UsableFactory
    {
        private readonly IObjectResolver _objectResolver;

        public UsableFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public IUsable Create(InventoryItemDataSO itemData, Transform parent)
        {
            var usableItem = _objectResolver.Instantiate(itemData.UsablePrefab, parent).GetComponent<IUsable>();
            usableItem.transform.localPosition = itemData.UsablePositionOffset;
            usableItem.transform.localEulerAngles = itemData.UsableRotationOffset;
            usableItem.gameObject.SetActive(false);
            return usableItem;
        }
    }
}