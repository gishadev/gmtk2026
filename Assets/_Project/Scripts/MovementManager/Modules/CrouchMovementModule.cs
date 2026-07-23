using Cysharp.Threading.Tasks;
using gishadev.gmtk.MovementManager.Contexts;
using UnityEngine;

namespace gishadev.gmtk.MovementManager.Modules
{
    public class CrouchMovementModule : IMovementModule
    {
        private readonly PlayerMovementContext _context;

        public CrouchMovementModule(PlayerMovementContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            _context.InputService.CrouchPerformed += OnCrouchPerformed;
            _context.InputService.CrouchCancelled += OnCrouchCancelled;
        }

        public void Dispose()
        {
            _context.InputService.CrouchPerformed -= OnCrouchPerformed;
            _context.InputService.CrouchCancelled -= OnCrouchCancelled;
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        private void OnCrouchCancelled()
        {
            _context.IsCrouching = false;
            UncrouchAsync();
        }

        private void OnCrouchPerformed()
        {
            _context.IsCrouching = true;
            CrouchAsync();
        }

        private async void CrouchAsync()
        {
            float vel = 0;

            while (_context.IsCrouching)
            {
                float scaleY = Mathf.SmoothDamp(_context.Transform.localScale.y,
                    _context.CharacterMovementDataSO.CrouchHeight, ref vel, _context.CharacterMovementDataSO.CrouchSmoothness);
                float value = _context.Transform.localScale.y - scaleY;
                Vector3 pos = -Vector3.up * value;

                _context.Transform.localScale = new Vector3(1f, scaleY, 1f);
                _context.Transform.position += pos;

                await UniTask.Yield();
            }
        }

        private async void UncrouchAsync()
        {
            float vel = 0;

            while (!_context.IsCrouching)
            {
                float scaleY = Mathf.SmoothDamp(_context.Transform.localScale.y, 1f, ref vel,
                    _context.CharacterMovementDataSO.CrouchSmoothness);
                float value = _context.Transform.localScale.y - scaleY;
                Vector3 pos = -Vector3.up * value;

                _context.Transform.localScale = new Vector3(1f, scaleY, 1f);
                _context.Transform.position += pos;

                await UniTask.Yield();
            }
        }
    }
}