using gishadev.gmtk.MovementManager.Contexts;
using UnityEngine;

namespace gishadev.gmtk.MovementManager.Modules.Rigidbody
{
    public class RigidbodyJumpModule : IMovementModule
    {
        private readonly RigidbodyPlayerMovementContext _context;
        private float JumpMultiplier => _context.IsCrouching ? 0.5f : 1f;
        public bool IsFixedTick => false;

        public RigidbodyJumpModule(RigidbodyPlayerMovementContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.InputService.JumpPerformed += OnJumpPerformed;
        }

        public void Dispose()
        {
            _context.InputService.JumpPerformed -= OnJumpPerformed;
        }

        public void Tick()
        {
            if (!_context.IsJumping)
                _context.Collider.material = _context.IsGrounded
                    ? _context.CharacterMovementDataSO.GroundedPhysicsMaterial
                    : _context.CharacterMovementDataSO.AirPhysicsMaterial;
        }

        public void FixedTick()
        {
        }

        private void OnJumpPerformed()
        {
            if (_context.IsGrounded && !_context.IsJumping)
                Jump();
        }

        private void Jump()
        {
            _context.IsJumping = true;

            Vector3 jumpVelocity = CalculateJumpVelocity(_context.MoveDir * _context.NowMoveSpeed);
            _context.Rigidbody.linearVelocity = jumpVelocity;
            _context.Velocity = jumpVelocity;
            _context.Collider.material = _context.CharacterMovementDataSO.AirPhysicsMaterial;
        }

        private Vector3 CalculateJumpVelocity(Vector3 moveVel)
        {
            Vector3 currentVelocity = _context.Rigidbody.linearVelocity;
            Vector3 vel;

            vel.x = moveVel.x * _context.GetMovementMultiplier();
            vel.z = moveVel.z * _context.GetMovementMultiplier();

            // Calculate vertical jump velocity
            float jumpForce = _context.CharacterMovementDataSO.JumpForce * JumpMultiplier;
            vel.y = Mathf.Sqrt(jumpForce * -2f * Physics.gravity.y *
                               _context.CharacterMovementDataSO.GravityMultiplier);

            vel.x += currentVelocity.x;
            vel.z += currentVelocity.z;

            return vel;
        }
    }
}