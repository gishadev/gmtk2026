using gishadev.gmtk.MovementManager.Contexts;

namespace gishadev.gmtk.MovementManager.Modules
{
    public class SprintMovementModule : IMovementModule
    {
        private readonly PlayerMovementContext _context;

        public SprintMovementModule(PlayerMovementContext context)
        {
            _context = context;
        }
        
        public void Initialize()
        {
            _context.InputService.SprintPerformed += OnSprintPerformed;
            _context.InputService.SprintCancelled += OnSprintCancelled;
        }

        public void Dispose()
        {
            _context.InputService.SprintPerformed -= OnSprintPerformed;
            _context.InputService.SprintCancelled -= OnSprintCancelled;
        }

        public void Tick()
        {
        }

        public void FixedTick()
        {
        }

        private void OnSprintCancelled() => _context.IsSprinting = false;
        private void OnSprintPerformed() => _context.IsSprinting = !_context.IsCrouching;
    }
}