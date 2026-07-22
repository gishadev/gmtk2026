using System.Collections.Generic;
using System.Linq;
using gishadev.walkingSimulator.EventsManager;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gishadev.walkingSimulator.UI
{
    public class InventoryGUIHandler : MonoBehaviour
    {
        [SerializeField] private Transform slotsParent;

        [Inject] private GameUIDataSO _gameUIDataSO;
        [Inject] private IEventBus _eventBus;
        [Inject] private IObjectResolver _objectResolver;

        private readonly List<InventorySlotGUIHandler> _inventorySlotsGUI = new();
        private bool _isInitialized;

        private void OnEnable()
        {
            _eventBus.Subscribe<InventoryUpdatedEvent>(OnInventoryUpdated);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<InventoryUpdatedEvent>(OnInventoryUpdated);
        }

        private void OnInventoryUpdated(InventoryUpdatedEvent obj)
        {
            if (!_isInitialized)
                InitializeInventoryGUI(obj);
            else
                UpdateInventoryGUI(obj);
        }

        private void InitializeInventoryGUI(InventoryUpdatedEvent obj)
        {
            for (int i = 0; i < obj.InventoryUsables.Count(); i++)
            {
                var slotHandler = _objectResolver.Instantiate(_gameUIDataSO.InventorySlotPrefab, slotsParent)
                    .GetComponent<InventorySlotGUIHandler>();
                slotHandler.HideContent();
                _inventorySlotsGUI.Add(slotHandler);
            }

            _isInitialized = true;
        }

        private void UpdateInventoryGUI(InventoryUpdatedEvent obj)
        {
            for (int i = 0; i < obj.InventoryUsables.Count(); i++)
            {
                var usable = obj.InventoryUsables.ToArray()[i];
                _inventorySlotsGUI[i].SetEquipped(i == obj.EquippedIndex);
                if (usable == null)
                {
                    _inventorySlotsGUI[i].HideContent();
                    continue;
                }
                _inventorySlotsGUI[i].UpdateSlotContent(usable.InventoryItemData);
            }
        }
    }
}