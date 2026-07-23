using UnityEngine;
using VContainer;

namespace gishadev.gmtk.Core
{
    [RequireComponent(typeof(AstarPath))]
    public class LocationGraphScanner : MonoBehaviour
    {
        [Inject] private ILocationController _locationController;

        private void Start()
        {
            _locationController.LocationLoaded += OnLocationLoaded;

            // The first location loads from a VContainer entry point, before this Start runs,
            // so its event was already dispatched. Scan the current one now to catch up.
            if (_locationController.CurrentLocation != null)
                RescanGraph();
        }

        private void OnDestroy() => _locationController.LocationLoaded -= OnLocationLoaded;

        private void OnLocationLoaded(Location location) => RescanGraph();

        private void RescanGraph()
        {
            if (AstarPath.active == null)
                return;

            // Flush the freshly instantiated location's colliders into the physics world,
            // otherwise the scan runs before they exist and finds nothing.
            Physics.SyncTransforms();
            AstarPath.active.Scan();
        }
    }
}
