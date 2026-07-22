using gishadev.walkingSimulator.Core;
using UnityEngine;

namespace gishadev.walkingSimulator.MovementManager
{
    public class RigidbodyPlayerMovementContext : PlayerMovementContext
    {
        public Rigidbody Rigidbody { get; }
        public Collider Collider { get; }
        
        public RigidbodyPlayerMovementContext(Rigidbody rigidbody, Collider collider, Transform transform, IPlayerInputService inputService,
            CharacterMovementDataSO characterMovementDataSO) : base(transform, inputService, characterMovementDataSO)
        {
            Rigidbody = rigidbody;
            Collider = collider;
        }
    }
}