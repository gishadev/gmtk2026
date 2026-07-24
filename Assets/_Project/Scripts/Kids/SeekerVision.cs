using System.Linq;
using gishadev.gmtk.Core;
using gishadev.walkingSimulator.EventsManager;
using UnityEngine;
using VContainer;

namespace gishadev.gmtk.kids
{
    /// <summary>
    /// Placed on the player. During the seeking phase, spots hiding kids that are
    /// both close enough and inside the camera's view cone with a clear line of sight.
    /// </summary>
    public class SeekerVision : MonoBehaviour
    {
        [Inject] private IKidsController _kidsController;
        [Inject] private KidsDataSO _kidsData;
        [Inject] private IEventBus _eventBus;

        private Camera _cam;
        private bool _isKidInRadius;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (!_kidsController.IsSeeking)
                return;

            var camTransform = _cam.transform;
            var origin = camTransform.position;
            var forward = camTransform.forward;

            var kidInRadius = false;

            // Snapshot: a found kid drops out of HidingKids immediately, but we may
            // find several this frame.
            foreach (var kid in _kidsController.HidingKids.ToArray())
            {
                if (kid == null)
                    continue;

                var toKid = kid.transform.position - origin;
                var distance = toKid.magnitude;
                if (distance > _kidsData.DetectRadius)
                    continue;

                kidInRadius = true;

                if (Vector3.Angle(forward, toKid) > _kidsData.ViewHalfAngle)
                    continue;

                // Line of sight: something on the blocking mask between us and the kid hides it.
                if (Physics.Raycast(origin, toKid.normalized, out var hit, distance, _kidsData.LineOfSightMask)
                    && hit.transform != kid.transform && !hit.transform.IsChildOf(kid.transform))
                    continue;

                _kidsController.NotifyKidFound(kid);
            }

            NotifySeekerRadius(kidInRadius);
        }

        /// <summary>
        /// Publishes only on transitions: once with <c>true</c> when a kid first enters
        /// the radius, once with <c>false</c> when the last one leaves it.
        /// </summary>
        private void NotifySeekerRadius(bool isInRadius)
        {
            if (isInRadius == _isKidInRadius)
                return;

            _isKidInRadius = isInRadius;
            _eventBus.Publish(new SeekRadiusWithKidChangedEvent(isInRadius));
        }
    }
}