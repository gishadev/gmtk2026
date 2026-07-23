namespace gishadev.gmtk.Interactions.EnvironmentInteractionManager
{
    public interface IInteractable
    {
        void Interact();
        bool CanInteract { get; }
    }
}