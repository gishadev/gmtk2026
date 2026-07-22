using System;
using System.Linq;
using gishadev.walkingSimulator.EventsManager;
using UnityEngine;
using VContainer;

namespace gishadev.walkingSimulator.Interactions.InventoryManager
{
    public abstract class InventorySystem
    {
        [Inject] private IEventBus _eventBus;
        public IUsable[] Usables { get; protected set; }
        public bool IsFull => Usables.All(x => x != null);

        private readonly UsableFactory _usableFactory;
        private int _equippedIndex;
        
        protected InventorySystem(IObjectResolver objectResolver)
        {
            Usables = Array.Empty<IUsable>();
            objectResolver.Inject(this);
            _usableFactory = new UsableFactory(objectResolver);
        }

        public virtual void AddUsable(InventoryItemDataSO inventoryItemData, Transform parent)
        {
            PublishUpdateEvent();
        }

        public virtual void RemoveUsable(int usableIndex)
        {
            if (Usables.All(x => x == null))
                return;

            Usables[usableIndex].Dispose();
            Usables[usableIndex] = null;

            PublishUpdateEvent();
        }

        public void RemoveUsable(IUsable usable)
        {
            var usableIndex = Array.FindIndex(Usables, x => x == usable);
            if (usableIndex <= -1)
                return;

            RemoveUsable(usableIndex);
        }

        public void SetEquippedUsable(int index)
        {
            _equippedIndex = index;
            PublishUpdateEvent();
        }

        protected IUsable CreateUsable(InventoryItemDataSO itemData, Transform parent) =>
            _usableFactory.Create(itemData, parent);

        protected void PublishUpdateEvent()
            => _eventBus.Publish(new InventoryUpdatedEvent(Usables, _equippedIndex));
    }
}