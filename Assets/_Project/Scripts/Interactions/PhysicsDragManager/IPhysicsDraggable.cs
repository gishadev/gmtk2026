using UnityEngine;

namespace gishadev.walkingSimulator.PhysicsDragManager
{
    public interface IPhysicsDraggable
    {
        GameObject gameObject { get; }
        Transform transform { get; }
    }
}
