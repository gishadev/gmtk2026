using System;
using VContainer;
using VContainer.Unity;

namespace gishadev.gmtk.Core
{
    public class LocationController : ILocationController
    {
        public LocationData CurrentLocationData =>
            IsValidIndex(_currentIndex) ? _gameData.Locations[_currentIndex] : null;

        public Location CurrentLocation { get; private set; }
        public bool HasNextLocation => IsValidIndex(_currentIndex + 1);

        public event Action<Location> LocationLoaded;

        private readonly IObjectResolver _objectResolver;
        private readonly GameDataSO _gameData;

        private int _currentIndex = -1;
        private PlayerCharacter _playerCharacter;

        public LocationController(IObjectResolver objectResolver, GameDataSO gameData)
        {
            _objectResolver = objectResolver;
            _gameData = gameData;
        }

        public void LoadFirstLocation()
        {
            _currentIndex = 0;
            LoadCurrentLocation();
        }

        public void ChangeToNextLocation()
        {
            if (!HasNextLocation)
                return;

            _currentIndex++;
            LoadCurrentLocation();
        }

        private void LoadCurrentLocation()
        {
            if (CurrentLocationData == null)
                return;

            if (CurrentLocation != null)
                UnityEngine.Object.Destroy(CurrentLocation.gameObject);

            // Instantiate via the resolver so the location's scene objects (e.g. the exit zone) get injected.
            CurrentLocation = _objectResolver.Instantiate(CurrentLocationData.LocationPrefab);

            SpawnOrMovePlayer();

            LocationLoaded?.Invoke(CurrentLocation);
        }

        private void SpawnOrMovePlayer()
        {
            var spawn = CurrentLocation.PlayerSpawnTrans;
            if (spawn == null)
                return;

            var player = GetPlayer();
            if (player != null)
            {
                player.Teleport(spawn.position, spawn.rotation);
                return;
            }

            // No character yet -> spawn one (injected by the resolver) at this location's spawn point.
            if (_gameData.CharacterPrefab == null)
            {
                UnityEngine.Debug.LogWarning(
                    "LocationController: GameDataSO.CharacterPrefab is not set; no character to spawn.");
                return;
            }

            var instance = _objectResolver.Instantiate(_gameData.CharacterPrefab, spawn.position, spawn.rotation);
            _playerCharacter = instance.GetComponent<PlayerCharacter>();
        }

        private PlayerCharacter GetPlayer()
        {
            if (_playerCharacter == null)
                _playerCharacter = UnityEngine.Object.FindAnyObjectByType<PlayerCharacter>();

            return _playerCharacter;
        }

        private bool IsValidIndex(int index) =>
            _gameData.Locations != null && index >= 0 && index < _gameData.Locations.Length;
    }
}
