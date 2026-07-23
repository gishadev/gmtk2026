using UnityEngine;

namespace gishadev.gmtk.kids
{
    /// <summary>
    /// A destination a found kid flees to. On arrival the kid leaves the game (disappears).
    /// Shared: several kids may run to the same one, so it is never "occupied".
    /// </summary>
    public class NextLocationPOI : MonoBehaviour, IPOI
    {
        public bool IsOccupied => false;

        public void Occupy()
        {
        }

        public void Free()
        {
        }
    }
}