using System;
using UnityEngine;

namespace gishadev.gmtk.Input
{
    public interface IPlayerInputService
    {
        event Action<Vector2> MovementPerformed;
        event Action MovementCancelled;
        event Action JumpPerformed;
        
        event Action CrouchPerformed;
        event Action CrouchCancelled;

        event Action SprintPerformed;
        event Action SprintCancelled;
        
        event Action PickPerformed;
        event Action PickCancelled;
        event Action InteractPerformed;
        event Action DropPerformed;
        event Action UsePerformed;
        event Action NextUsablePerformed;
        event Action PreviousUsablePerformed;

        /// <summary>Enables/disables player gameplay input (e.g. locked during a location transition).</summary>
        void SetInputEnabled(bool enabled);
    }
}