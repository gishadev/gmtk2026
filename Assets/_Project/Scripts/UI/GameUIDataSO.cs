using UnityEngine;

namespace gishadev.walkingSimulator.UI
{
    [CreateAssetMenu(fileName = "GameUIData", menuName = "ScriptableObjects/WalkingSim/GameUIData")]
    public class GameUIDataSO : ScriptableObject
    {
        [field: SerializeField] public GameObject OverlayHintPrefab { get; private set; }
        [field: SerializeField] public GameObject InventorySlotPrefab { get; private set; }
    }
}