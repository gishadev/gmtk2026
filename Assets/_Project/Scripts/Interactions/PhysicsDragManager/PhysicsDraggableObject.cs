using UnityEngine;

namespace gishadev.walkingSimulator.PhysicsDragManager
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PhysicsDraggableObject : MonoBehaviour, IPhysicsDraggable
    {
    }
}