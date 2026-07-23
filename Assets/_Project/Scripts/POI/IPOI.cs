using UnityEngine;

namespace gishadev.gmtk.kids
{
    public interface IPOI
    {
        Transform transform { get; }
        bool IsOccupied { get; }
        void Occupy();
        void Free();
    }
}
