using UnityEngine;

namespace gishadev.gmtk.Interactions.PhysicsDragManager
{
    public interface IPhysicsDraggable
    {
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}
