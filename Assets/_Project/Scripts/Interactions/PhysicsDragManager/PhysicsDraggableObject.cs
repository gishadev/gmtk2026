using UnityEngine;

namespace gishadev.gmtk.Interactions.PhysicsDragManager
{
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class PhysicsDraggableObject : MonoBehaviour, IPhysicsDraggable
    {
    }
}