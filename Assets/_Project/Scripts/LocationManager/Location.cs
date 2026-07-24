using System;
using gishadev.gmtk.kids;
using UnityEngine;

namespace gishadev.gmtk.LocationManager
{
    public class Location : MonoBehaviour
    {
        [field: SerializeField] public Transform PlayerSpawnTrans { get; private set; }
        [field: SerializeField] public KidHidingSpot[] HidingSpots { get; private set; }
        [field: SerializeField] public NextLocationPOI NextLocationPOI { get; private set; }
        [field: SerializeField] public LocationExitZone ExitZone { get; private set; }

        /// <summary>Populates the serialized references by scanning child objects.</summary>
        public void AutoFillFromChildren()
        {
            HidingSpots = GetComponentsInChildren<KidHidingSpot>(true);
            NextLocationPOI = GetComponentInChildren<NextLocationPOI>(true);
            ExitZone = GetComponentInChildren<LocationExitZone>(true);

            var spawn = FindDescendant("spawn");
            if (spawn != null)
                PlayerSpawnTrans = spawn;
        }

        private Transform FindDescendant(string nameContains)
        {
            foreach (var t in GetComponentsInChildren<Transform>(true))
                if (t != transform && t.name.IndexOf(nameContains, StringComparison.OrdinalIgnoreCase) >= 0)
                    return t;

            return null;
        }
    }
}
