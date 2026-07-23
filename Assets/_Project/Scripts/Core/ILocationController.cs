using System;

namespace gishadev.gmtk.Core
{
    public interface ILocationController
    {
        /// <summary>Config for the location the game is currently on (null before the first load).</summary>
        LocationData CurrentLocationData { get; }

        /// <summary>The spawned instance of the current location (null until actually spawned).</summary>
        Location CurrentLocation { get; }

        bool HasNextLocation { get; }

        event Action<Location> LocationLoaded;

        void LoadFirstLocation();
        void ChangeToNextLocation();
    }
}
