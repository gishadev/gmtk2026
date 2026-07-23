using gishadev.gmtk.MovementManager.Contexts;
using UnityEngine;

namespace gishadev.gmtk.MovementManager.Modules.Kinematic
{
    public class KinematicJumpModule : IMovementModule
    {
        private readonly KinematicPlayerMovementContext _context;
        private float JumpMultiplier => _context.IsCrouching ? 0.5f : 1f;
        public bool IsFixedTick => false;

        public KinematicJumpModule(KinematicPlayerMovementContext context)
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
        }

        public void FixedTick()
        {
        }

        private void OnJumpPerformed()
        {
            if (_context.IsGrounded)
                Jump();
        }

        private void Jump()
        {
            _context.IsJumping = true;
            _context.Velocity = CalculateJumpVelocity(_context.MoveDir * _context.NowMoveSpeed);
            _context.Controller.slopeLimit = 90f;
        }

        private Vector3 CalculateJumpVelocity(Vector3 moveVel)
        {
            Vector3 vel;

            vel.x = moveVel.x * _context.GetMovementMultiplier();
            vel.y = Mathf.Sqrt((_context.CharacterMovementDataSO.JumpForce * JumpMultiplier) * -2f * Physics.gravity.y);
            vel.z = moveVel.z * _context.GetMovementMultiplier();

            return vel;
        }


    }
}