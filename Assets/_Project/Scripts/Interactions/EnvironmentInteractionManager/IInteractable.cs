namespace gishadev.walkingSimulator.Interactions.EnvironmentInteractionManager
{
    public interface IInteractable
    {
        void Interact();
        bool CanInteract { get; }
    }
}