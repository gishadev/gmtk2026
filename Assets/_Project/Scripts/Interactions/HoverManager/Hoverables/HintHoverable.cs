using gishadev.walkingSimulator.EventsManager;
using UnityEngine;
using UnityEngine.Localization;
using VContainer;

namespace gishadev.walkingSimulator.InteractionManager
{
    /// <summary>
    /// Show hints on hovered.
    /// </summary>
    public class HintHoverable : MonoBehaviour, IHoverable
    {
        [SerializeField] private LocalizedString hintText;

        [Inject] private IEventBus _eventBus;

        public void OnHoverEnter()
        {
            _eventBus.Publish(new HintRequestedEvent(
                hintText.GetLocalizedString(),
                transform
            ));
        }

        public void OnHoverExit()
        {
            _eventBus.Publish(new HintDismissedEvent());
        }
    }
}