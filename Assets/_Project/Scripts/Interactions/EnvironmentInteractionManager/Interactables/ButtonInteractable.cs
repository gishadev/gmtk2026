using UnityEngine;

namespace gishadev.walkingSimulator.Interactions.EnvironmentInteractionManager
{
    public class ButtonInteractable : MonoBehaviour, IInteractable
    {
        public bool CanInteract => true;

        public void Interact()
        {
            if (!CanInteract)
                return;

            Debug.Log("button interacted yipee!");
        }
    }
}