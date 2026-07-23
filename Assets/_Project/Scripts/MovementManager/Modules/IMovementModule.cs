namespace gishadev.gmtk.MovementManager.Modules
{
    public interface IMovementModule
    {
        void Initialize();
        void Dispose();
        void Tick();
        void FixedTick();
    }
}