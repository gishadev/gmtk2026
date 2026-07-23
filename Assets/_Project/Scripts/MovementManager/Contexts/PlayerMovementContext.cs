using gishadev.gmtk.Core;
using UnityEngine;

namespace gishadev.gmtk.MovementManager.Contexts
{
    public abstract class PlayerMovementContext
    {
        protected PlayerMovementContext(Transform transform, IPlayerInputService inputService,
            CharacterMovementDataSO characterMovementDataSO)
        {
            Transform = transform;
            InputService = inputService;
            CharacterMovementDataSO = characterMovementDataSO;
        }

        public bool IsGrounded { get; set; }
        public bool IsJumping { get; set; }
        public bool IsSprinting { get; set; }
        public bool IsCrouching { get; set; }
        public Vector3 Velocity { get; set; }
        public Vector2 RawInput { get; set; }
        public Vector3 MoveDir { get; set; }

        public float NowMoveSpeed => IsSprinting ? CharacterMovementDataSO.SprintSpeed : CharacterMovementDataSO.WalkSpeed;

        public Transform Transform { get; }
        public IPlayerInputService InputService { get; }
        public CharacterMovementDataSO CharacterMovementDataSO { get; }


        public float GetMovementMultiplier()
        {
            float n1 = 1f, n2 = 1f;

            if (!IsGrounded)
                n1 = CharacterMovementDataSO.AirMovementMultiplier;
            if (IsCrouching)
                n2 = CharacterMovementDataSO.CrouchMovementMultiplier;

            return n1 * n2;
        }
    }
}