using System.Linq;
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

        private Camera _cam;

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

                if (Vector3.Angle(forward, toKid) > _kidsData.ViewHalfAngle)
                    continue;

                // Line of sight: something on the blocking mask between us and the kid hides it.
                if (Physics.Raycast(origin, toKid.normalized, out var hit, distance, _kidsData.LineOfSightMask)
                    && hit.transform != kid.transform && !hit.transform.IsChildOf(kid.transform))
                    continue;

                _kidsController.NotifyKidFound(kid);
            }
        }
    }
}
