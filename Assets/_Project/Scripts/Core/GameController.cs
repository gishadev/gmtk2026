using System;
using gishadev.gmtk.Input;
using gishadev.gmtk.LocationManager;
using Cysharp.Threading.Tasks;
using gishadev.gmtk.Countdown;
using gishadev.gmtk.kids;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace gishadev.gmtk.Core
{
    public class GameController : IGameController, IInitializable, IDisposable
    {
        [Inject] private IKidsController _kidsController;
        [Inject] private ILocationController _locationController;
        [Inject] private IPlayerInputService _playerInputService;
        [Inject] private ICountdownController _countdownController;

        public void Initialize()
        {
            _kidsController.AllKidsFound += OnAllKidsFound;
            _locationController.LocationLoaded += OnLocationLoaded;

            StartGameAsync().Forget();
        }

        public void Dispose()
        {
            _kidsController.AllKidsFound -= OnAllKidsFound;
            _locationController.LocationLoaded -= OnLocationLoaded;
        }

        // Runs once at the start of the Game (not per round): locks the player out,
        // runs the intro countdown, then hands control back.
        private async UniTaskVoid StartGameAsync()
        {
            _playerInputService.SetInputEnabled(false);

            _locationController.LoadFirstLocation();

            await _countdownController.StartCountdown();

            _playerInputService.SetInputEnabled(true);
        }

        private void StartRound()
        {
            _kidsController.SpawnAndHideKids();
            // Kids are findable from the moment they spawn - no hide timer to wait out.
            _kidsController.BeginSeeking();
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
                Debug.Log("GameController: all locations cleared - game won!");
            }
        }

        private void OnLocationLoaded(Location location) => StartRound();
    }
}
