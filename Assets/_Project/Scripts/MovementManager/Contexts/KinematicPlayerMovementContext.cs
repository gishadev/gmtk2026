using gishadev.gmtk.Core;
using UnityEngine;

namespace gishadev.gmtk.MovementManager.Contexts
{
    public class KinematicPlayerMovementContext : PlayerMovementContext
    {
        public CharacterController Controller { get; }

        public KinematicPlayerMovementContext(CharacterController controller, Transform transform,
            IPlayerInputService inputService,
            CharacterMovementDataSO characterMovementDataSO) : base(transform, inputService, characterMovementDataSO)
        {
            Controller = controller;
        }
    }
}