using UnityEngine;

namespace gishadev.walkingSimulator.MovementManager.Modules
{
    public class RigidbodyWalkModule : IMovementModule
    {
        private readonly RigidbodyPlayerMovementContext _context;
        private readonly Transform _groundCheckerPoint;
        public bool IsFixedTick => true;

        // If On Slope => Additional scale for gravity to reduce "bouncing" bug.
        public float SlopeMultiplier => IsOnSlope() ? _context.CharacterMovementDataSO.SlopeForce : 1f;

        public RigidbodyWalkModule(RigidbodyPlayerMovementContext context, Transform groundCheckerPoint)
        {
            _context = context;
            _groundCheckerPoint = groundCheckerPoint;
        }

        public void Initialize()
        {
            _context.InputService.MovementPerformed += OnMovementPerformed;
            _context.InputService.MovementCancelled += OnMovementCancelled;

            // Configure Rigidbody settings
            _context.Rigidbody.freezeRotation = true; // Prevent rotation from physics
            _context.Rigidbody.useGravity = false; // We'll handle gravity manually
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
            Vector3 currentVelocity = _context.Rigidbody.linearVelocity;

            // When player is on ground.
            if (_context.IsGrounded && currentVelocity.y < 0f)
            {
                if (_context.IsJumping)
                    _context.IsJumping = false;

                currentVelocity.y = -2f;
            }

            _context.MoveDir = _context.Transform.forward * _context.RawInput.y
                               + _context.Transform.right * _context.RawInput.x;

            // Apply slope handling for movement direction
            Vector3 targetMovement = GetSlopeAdjustedMovement();

            // Set horizontal velocity while preserving vertical velocity
            currentVelocity.x = targetMovement.x;
            currentVelocity.z = targetMovement.z;

            currentVelocity = ApplyGravity(currentVelocity);
            _context.Rigidbody.linearVelocity = currentVelocity;

            _context.Velocity = currentVelocity;
        }

        private Vector3 ApplyGravity(Vector3 currentVelocity)
        {
            if (!_context.IsGrounded || currentVelocity.y > 0f)
            {
                currentVelocity.y += Physics.gravity.y * _context.CharacterMovementDataSO.GravityMultiplier *
                                     SlopeMultiplier * Time.fixedDeltaTime;
            }

            return currentVelocity;
        }

        private Vector3 GetSlopeAdjustedMovement()
        {
            Vector3 movement = Vector3.ClampMagnitude(_context.MoveDir, 1f) *
                               (_context.NowMoveSpeed * _context.GetMovementMultiplier());

            // If not grounded or no movement input, return as-is
            if (!_context.IsGrounded || movement.magnitude == 0f)
                return movement;

            Vector3 groundNormal = GetGroundNormal();
            if (groundNormal == Vector3.up)
                return movement;

            Vector3 slopeDirection = Vector3.ProjectOnPlane(movement, groundNormal).normalized;
            Vector3 adjustedMovement = slopeDirection * movement.magnitude;

            float slopeAngle = Vector3.Angle(Vector3.up, groundNormal);
            float maxSlopeAngle = _context.CharacterMovementDataSO.MaxSlopeAngle;

            if (slopeAngle > maxSlopeAngle)
            {
                // On slopes too steep, reduce movement or slide down
                Vector3 slideDirection = Vector3.ProjectOnPlane(Vector3.down, groundNormal);
                float slideForce = _context.CharacterMovementDataSO.SlopeForce;
                adjustedMovement = Vector3.Lerp(adjustedMovement, slideDirection * slideForce,
                    (slopeAngle - maxSlopeAngle) / (90f - maxSlopeAngle));
            }

            return adjustedMovement;
        }

        private Vector3 GetGroundNormal()
        {
            if (Physics.Raycast(_groundCheckerPoint.position, Vector3.down, out var hitInfo,
                    _context.CharacterMovementDataSO.GroundCheckerRadius + 0.1f,
                    _context.CharacterMovementDataSO.GroundMask))
                return hitInfo.normal;

            if (Physics.SphereCast(_groundCheckerPoint.position,
                    _context.CharacterMovementDataSO.GroundCheckerRadius * 0.5f,
                    Vector3.down, out hitInfo, _context.CharacterMovementDataSO.GroundCheckerRadius + 0.1f,
                    _context.CharacterMovementDataSO.GroundMask))
                return hitInfo.normal;

            return Vector3.up;
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
            return Physics.CheckSphere(_groundCheckerPoint.position,
                _context.CharacterMovementDataSO.GroundCheckerRadius,
                _context.CharacterMovementDataSO.GroundMask);
        }

        private void OnMovementCancelled() => _context.RawInput = Vector3.zero;
        private void OnMovementPerformed(Vector2 obj) => _context.RawInput = obj;
    }
}