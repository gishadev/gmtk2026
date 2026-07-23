using gishadev.gmtk.Core;
using gishadev.gmtk.MovementManager.Contexts;
using UnityEngine;

namespace gishadev.gmtk.MovementManager.Modules
{
    public class MovingPlatformModule : IMovementModule
    {
        private readonly PlayerMovementContext _context;
        private readonly Transform _groundCheckerPoint;

        private RaycastHit _hitInfo;

        public MovingPlatformModule(PlayerMovementContext context, Transform groundCheckerPoint)
        {
            _context = context;
            _groundCheckerPoint = groundCheckerPoint;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public void Tick()
        {
            if (CheckGroundMovingPlatform() && _context.Transform.parent != _hitInfo.transform)
            {
                _context.Transform.SetParent(_hitInfo.transform);
                return;
            }

            if (!CheckGroundMovingPlatform() && _context.Transform.parent != null)
                _context.Transform.SetParent(null);
        }

        public void FixedTick()
        {
        }

        private bool CheckGroundMovingPlatform()
        {
            var isGround = Physics.Raycast(_groundCheckerPoint.position,
                Vector3.down, out _hitInfo, _context.CharacterMovementDataSO.GroundCheckerRadius * 2f,
                _context.CharacterMovementDataSO.GroundMask);
            return isGround && _hitInfo.collider.CompareTag(Constants.MOVING_PLATFORM_TAG);
        }
    }
}