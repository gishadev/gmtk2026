using UnityEngine;

namespace gishadev.walkingSimulator.InteractionManager
{
    [CreateAssetMenu(fileName = "CharacterInteractionDataSO", menuName = "ScriptableObjects/WalkingSim/CharacterInteractionDataSO")]
    public class CharacterInteractionDataSO : ScriptableObject
    {
        [field: SerializeField] public float UniversalInteractRadius { get; private set; } = .5f;
        
        [Header("Inventory/Pick Up Interactions")]
        [field: SerializeField] public float PickableHitRange { get; private set; } = 3f;
        [field: SerializeField] public float PickableDropForce { get; private set; } = 5f;
        
        [Header("Inventory/Pick Up Interactions")]
        [field: SerializeField] public float EnvironmentInteractDistance { get; private set; } = 3f;
        
        [Header("Physics interaction")]
        [field: SerializeField] public float PhysicsPickableForce { get; private set; } = 600f;
        [field: SerializeField] public float PhysicsPickableDamping { get; private set; } = 6f;
        [field: SerializeField] public float PhysicsPickableDistance { get; private set; } = 15f;
        [field: SerializeField] public float PhysicsPickableMaxDragDistance { get; private set; } = 2f;
    }
}