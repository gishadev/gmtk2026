using System;
using gishadev.walkingSimulator.EventsManager;
using gishadev.walkingSimulator.UI;
using VContainer;
using VContainer.Unity;

namespace gishadev.gmtk.Core
{
    /// <summary>
    /// Listens for the player entering a location's exit zone and plays the fade-out /
    /// swap location / fade-in transition, locking input while it runs.
    /// </summary>
    public class LocationTransitionController : IInitializable, IDisposable
    {
        [Inject] private IEventBus _eventBus;
        [Inject] private IScreenFader _screenFader;
        [Inject] private ILocationController _locationController;
        [Inject] private IPlayerInputService _playerInputService;

        private bool _transitioning;

        public void Initialize() => _eventBus.Subscribe<LocationExitRequestedEvent>(OnExitRequested);
        public void Dispose() => _eventBus.Unsubscribe<LocationExitRequestedEvent>(OnExitRequested);

        private void OnExitRequested(LocationExitRequestedEvent _)
        {
            if (_transitioning || !_locationController.HasNextLocation)
                return;

            _transitioning = true;
            _playerInputService.SetInputEnabled(false);

            _screenFader.FadeThrough(
                whileBlack: () => _locationController.ChangeToNextLocation(),
                onComplete: () =>
                {
                    _playerInputService.SetInputEnabled(true);
                    _transitioning = false;
                });
        }
    }
}
