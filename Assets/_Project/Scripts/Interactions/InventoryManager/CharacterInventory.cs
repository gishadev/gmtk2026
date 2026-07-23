using gishadev.gmtk.Core;
using gishadev.gmtk.Interactions.InventoryManager.InventorySystems;
using gishadev.gmtk.Interactions.InventoryManager.Pickables;
using gishadev.gmtk.Interactions.InventoryManager.Usables;
using UnityEngine;
using VContainer;

namespace gishadev.gmtk.Interactions.InventoryManager
{
    public class CharacterInventory : MonoBehaviour
    {
        [SerializeField] private Transform cameraTrans;
        [SerializeField] private Transform handTrans;

        [Inject] private CharacterInteractionDataSO _characterInteractionDataSO;
        [Inject] private IPlayerInputService _inputService;
        [Inject] private IObjectResolver _objectResolver;

        private IUsable CurrentUsable => _inventorySystem.Usables[_equippedIndex];

        private InventorySystem _inventorySystem;
        private PickableFactory _pickableFactory;
        private Camera _cam;

        private int _equippedIndex;

        private void Awake()
        {
            _cam = Camera.main;
            _pickableFactory = new PickableFactory(_objectResolver);
        }

        private void Start()
        {
            _inventorySystem = new HotbarIS(_objectResolver);
            OnEquip(0);
        }

        private void OnEnable()
        {
            _inputService.InteractPerformed += OnInteractPerformed;
            _inputService.UsePerformed += OnUsePerformed;
            _inputService.DropPerformed += OnDropPerformed;

            _inputService.NextUsablePerformed += OnNextUsablePerformed;
            _inputService.PreviousUsablePerformed += OnPreviousUsablePerformed;
        }

        private void OnDisable()
        {
            _inputService.InteractPerformed -= OnInteractPerformed;
            _inputService.UsePerformed -= OnUsePerformed;
            _inputService.DropPerformed -= OnDropPerformed;

            _inputService.NextUsablePerformed -= OnNextUsablePerformed;
            _inputService.PreviousUsablePerformed -= OnPreviousUsablePerformed;
        }

        private void OnInteractPerformed()
        {
            if (!Physics.SphereCast(cameraTrans.position, _characterInteractionDataSO.UniversalInteractRadius,
                    cameraTrans.forward, out var hit,
                    _characterInteractionDataSO.PickableHitRange))
                return;

            if (hit.collider != null && !_inventorySystem.IsFull)
            {
                var pickableItem = hit.collider.GetComponent<IPickable>();
                if (pickableItem != null)
                {
                    pickableItem.OnPickup(handTrans);
                    _inventorySystem.AddUsable(pickableItem.InventoryItemData, handTrans);
                    OnEquip(_equippedIndex);
                    Destroy(pickableItem.gameObject);
                }
            }
        }

        private void OnDropPerformed()
        {
            if (CurrentUsable == null)
                return;

            var pickable = _pickableFactory.Create(CurrentUsable.InventoryItemData, handTrans);

            pickable.Rigidbody.AddForce(_characterInteractionDataSO.PickableDropForce * _cam.transform.forward,
                ForceMode.Impulse);
            _inventorySystem.RemoveUsable(CurrentUsable);
        }

        private void OnUsePerformed()
        {
            if (CurrentUsable == null)
                return;

            CurrentUsable.Use();

            if (CurrentUsable.InventoryItemData.IsSingleUse)
                _inventorySystem.RemoveUsable(CurrentUsable);

            Debug.Log("Use");
        }

        private void OnDequip()
        {
            CurrentUsable?.gameObject.SetActive(false);
        }

        private void OnEquip(int index)
        {
            _equippedIndex = index;
            CurrentUsable?.gameObject.SetActive(true);
            _inventorySystem.SetEquippedUsable(_equippedIndex);
        }

        private void OnNextUsablePerformed()
        {
            OnDequip();
            var tempIndex = _equippedIndex;
            tempIndex--;
            if (tempIndex < 0)
                tempIndex = _inventorySystem.Usables.Length - 1;

            OnEquip(tempIndex);
        }

        private void OnPreviousUsablePerformed()
        {
            OnDequip();
            var tempIndex = _equippedIndex;
            tempIndex++;
            if (tempIndex > _inventorySystem.Usables.Length - 1)
                tempIndex = 0;

            OnEquip(tempIndex);
        }
    }
}