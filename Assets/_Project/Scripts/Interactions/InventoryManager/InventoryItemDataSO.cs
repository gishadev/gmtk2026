using UnityEngine;
using UnityEngine.Localization;

namespace gishadev.gmtk.Interactions.InventoryManager
{
    [CreateAssetMenu(fileName = "InventoryItemData", menuName = "ScriptableObjects/WalkingSim/InventoryItemDataSO")]
    public class InventoryItemDataSO : ScriptableObject
    {
        [field: SerializeField] public LocalizedString ItemName { get; private set; }
        [field: SerializeField] public Sprite ItemIcon { get; private set; }
        [field: Space]
        [field: SerializeField] public bool IsSingleUse { get; private set; }
        [field: SerializeField] public GameObject UsablePrefab { get; private set; }
        [field: SerializeField] public Vector3 UsablePositionOffset { get; private set; }
        [field: SerializeField] public Vector3 UsableRotationOffset { get; private set; }
        [field: Space]
        [field: SerializeField] public GameObject PickablePrefab { get; private set; }
    }
}