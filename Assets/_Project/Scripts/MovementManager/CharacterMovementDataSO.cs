using UnityEngine;

namespace gishadev.gmtk.MovementManager
{
    [CreateAssetMenu(fileName = "CharacterMovementDataSO", menuName = "ScriptableObjects/WalkingSim/CharacterMovementDataSO")]
    public class CharacterMovementDataSO : ScriptableObject
    {
        [field: Header("Ground Checker")]
        [field: SerializeField] public float GroundCheckerRadius { get; private set; } = 0.5f;
        [field: SerializeField] public LayerMask GroundMask { get; private set; }

        [field: Header("Speeds/Forces")]
        [field: SerializeField] public float WalkSpeed { get; private set; } = 6f;
        [field: SerializeField] public float SprintSpeed { get; private set; } = 12f;
        [field: SerializeField] public float JumpForce { get; private set; } = 10f;

        [field: Header("Speeds/Forces")] 
        [field: SerializeField] public PhysicsMaterial AirPhysicsMaterial { get; private set; }
        [field: SerializeField] public PhysicsMaterial GroundedPhysicsMaterial { get; private set; }
        
        [field: Header("Multipliers")]
        [field: SerializeField] public float GravityMultiplier { get; private set; } = 4.5f;
        [field: Range(0f, 1f), SerializeField] public float AirMovementMultiplier { get; private set; } = 0.5f;
        [field: Range(0f, 1f), SerializeField] public float CrouchMovementMultiplier { get; private set; } = 0.5f;
        
        [field: Header("Slope")]
        [field: SerializeField] public float SlopeForce { get; private set; } = 7f;
        [field: SerializeField] public float MaxSlopeAngle { get; private set; } = 45f;
        [field: SerializeField] public float SlopeForceRayLength { get; private set; } = 1f;
        
        [field: Header("Crouch")]
        [field: SerializeField] public float CrouchHeight { get; private set; } = 0.5f;
        [field: SerializeField] public float CrouchSmoothness { get; private set; } = 0.1f;

    }
}