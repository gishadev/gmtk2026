using UnityEngine;

namespace gishadev.gmtk.kids
{
    public class KidHidingSpot : MonoBehaviour, IPOI
    {
        public bool IsOccupied { get; private set; }

        public void Occupy() => IsOccupied = true;
        public void Free() => IsOccupied = false;
    }
}
