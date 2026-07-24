using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace gishadev.gmtk.LocationManager
{
    [RequireComponent(typeof(AstarPath))]
    public class LocationGraphScanner : MonoBehaviour
    {
        [Inject] private ILocationController _locationController;

        private void Start()
        {
            _locationController.LocationLoaded += OnLocationLoaded;

            if (_locationController.CurrentLocation != null)
                RescanGraph();
        }

        private void OnDestroy() => _locationController.LocationLoaded -= OnLocationLoaded;

        private void OnLocationLoaded(Location location) => RescanGraph();

        private async void RescanGraph()
        {
            await UniTask.Yield();
            if (AstarPath.active == null)
                return;

            Physics.SyncTransforms();
            AstarPath.active.Scan();
        }
    }
}
