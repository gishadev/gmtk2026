using System;
using gishadev.gmtk.Core;
using gishadev.walkingSimulator.EventsManager;
using UnityEngine;
using VContainer;

namespace gishadev.gmtk.LocationManager
{
    /// <summary>
    /// Trigger area on a Location prefab. Once armed (the round is cleared), stepping into it
    /// as the player requests a transition to the next location.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class LocationExitZone : MonoBehaviour
    {
        [SerializeField] private GameObject arrowObject;
        [Inject] private IEventBus _eventBus;

        private bool _armed;
        private bool _fired;

        private void Start()
        {
            arrowObject.SetActive(false);
        }

        public void SetArmed(bool armed)
        {
            _armed = armed;
            if (armed)
            {
                arrowObject.SetActive(true);
                _fired = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_armed || _fired)
                return;

            if (other.GetComponentInParent<PlayerCharacter>() == null)
                return;

            _fired = true;
            _eventBus.Publish(new LocationExitRequestedEvent());
        }
    }
}