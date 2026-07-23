using System;
using gishadev.gmtk.kids;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gishadev.gmtk.Core
{
    public class GameController : IGameController, IInitializable, ITickable, IDisposable
    {
        private enum Phase
        {
            Countdown,
            Seeking,
            Won
        }

        [Inject] private IKidsController _kidsController;
        [Inject] private ILocationController _locationController;
        [Inject] private KidsDataSO _kidsData;

        private Phase _phase;
        private float _countdown;

        public void Initialize()
        {
            _kidsController.AllKidsFound += OnAllKidsFound;
            _locationController.LocationLoaded += OnLocationLoaded;

            _locationController.LoadFirstLocation();
        }

        public void Dispose()
        {
            _kidsController.AllKidsFound -= OnAllKidsFound;
            _locationController.LocationLoaded -= OnLocationLoaded;
        }

        private void StartRound()
        {
            _kidsController.SpawnAndHideKids();

            _countdown = _kidsData.HideCountdown;
            _phase = Phase.Countdown;
        }

        public void Tick()
        {
            if (_phase != Phase.Countdown)
                return;

            _countdown -= Time.deltaTime;
            if (_countdown <= 0f)
            {
                _phase = Phase.Seeking;
                _kidsController.BeginSeeking();
            }
        }

        private void OnAllKidsFound()
        {
            // More locations left -> arm the exit so the player can walk out; otherwise the game is won.
            if (_locationController.HasNextLocation)
            {
                if (_locationController.CurrentLocation != null &&
                    _locationController.CurrentLocation.ExitZone != null)
                    _locationController.CurrentLocation.ExitZone.SetArmed(true);
            }
            else
            {
                _phase = Phase.Won;
                Debug.Log("GameController: all locations cleared - game won!");
            }
        }

        private void OnLocationLoaded(Location location) => StartRound();
    }
}