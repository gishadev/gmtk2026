using UnityEngine;

namespace gishadev.walkingSimulator.MovementManager.Modules
{
    public class KinematicWalkModule : IMovementModule
    {
        private readonly KinematicPlayerMovementContext _context;
        private readonly Transform _groundCheckerPoint;
        public bool IsFixedTick => true;

        // If On Slope => Additional scale for gravity to reduce "bouncing" bug.
        public float SlopeMultiplier => IsOnSlope() ? _context.CharacterMovementDataSO.SlopeForce : 1f;

        public KinematicWalkModule(KinematicPlayerMovementContext context, Transform groundCheckerPoint)
        {
            _context = context;
            _groundCheckerPoint = groundCheckerPoint;
        }

        public void Initialize()
        {
            _context.InputService.MovementPerformed += OnMovementPerformed;
            _context.InputService.MovementCancelled += OnMovementCancelled;
        }

        public void Dispose()
        {
            _context.InputService.MovementPerformed -= OnMovementPerformed;
            _context.InputService.MovementCancelled -= OnMovementCancelled;
        }

        public void Tick()
        {
            _context.IsGrounded = CheckGroundCollider();
        }

        public void FixedTick()
        {
            // When player is on ground.
            if (_context.IsGrounded && _context.Velocity.y < 0f)
            {
                if (_context.IsJumping)
                    _context.IsJumping = false;

                _context.Velocity = Vector3.up * -2f;
                _context.Controller.slopeLimit = 50f;
            }

            _context.MoveDir = _context.Transform.forward * _context.RawInput.y 
                               + _context.Transform.right * _context.RawInput.x;
            
            _context.Controller.Move(Vector3.ClampMagnitude(_context.MoveDir, 1f) *
                                     (_context.NowMoveSpeed * _context.GetMovementMultiplier() * Time.deltaTime));

            // Applying gravity to player controller.
            var vVel = _context.Velocity.y + Physics.gravity.y * _context.CharacterMovementDataSO.GravityMultiplier *
                SlopeMultiplier * Time.deltaTime;
            _context.Velocity = new Vector3(_context.Velocity.x, vVel, _context.Velocity.z);
            _context.Controller.Move(_context.Velocity * Time.deltaTime);
        }

        private bool IsOnSlope()
        {
            if (!_context.IsGrounded || _context.IsJumping)
                return false;

            if (Physics.Raycast(_groundCheckerPoint.position, Vector3.down, out var hitInfo,
                    _context.CharacterMovementDataSO.SlopeForceRayLength))
                if (hitInfo.normal != Vector3.up)
                    return true;
            return false;
        }

        private bool CheckGroundCollider()
        {
            return Physics.CheckSphere(_groundCheckerPoint.position, _context.CharacterMovementDataSO.GroundCheckerRadius,
                _context.CharacterMovementDataSO.GroundMask);
        }

        private void OnMovementCancelled() => _context.RawInput = Vector3.zero;
        private void OnMovementPerformed(Vector2 obj) => _context.RawInput = obj;
    }
}