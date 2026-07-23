using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gishadev.gmtk.Interactions.InventoryManager.Pickables
{
    public class PickableFactory
    {
        private readonly IObjectResolver _objectResolver;

        public PickableFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public IPickable Create(InventoryItemDataSO itemData, Transform parent)
        {
            var pickable = _objectResolver.Instantiate(itemData.PickablePrefab).GetComponent<IPickable>();
            pickable.transform.position = parent.transform.position;
            return pickable;
        }
    }
}