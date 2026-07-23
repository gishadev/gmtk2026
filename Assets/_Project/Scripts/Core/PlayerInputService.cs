using System;
using gishadev.gmtk.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace gishadev.gmtk.Core
{
    public class PlayerInputService : IPlayerInputService, IInitializable, IDisposable
    {
        public event Action<Vector2> MovementPerformed;
        public event Action MovementCancelled;
        public event Action JumpPerformed;

        public event Action CrouchPerformed;
        public event Action CrouchCancelled;

        public event Action SprintPerformed;
        public event Action SprintCancelled;
        public event Action PickPerformed;
        public event Action PickCancelled;

        public event Action InteractPerformed;
        public event Action DropPerformed;
        public event Action UsePerformed;
        public event Action NextUsablePerformed;
        public event Action PreviousUsablePerformed;

        private CustomInput _customInput;

        public void Initialize()
        {
            _customInput = new CustomInput();
            _customInput.Enable();

            _customInput.Player.Movement.performed += OnMovementPerformed;
            _customInput.Player.Movement.canceled += OnMovementCancelled;

            _customInput.Player.Sprint.performed += OnSprintPerformed;
            _customInput.Player.Sprint.canceled += OnSprintCancelled;

            _customInput.Player.Crouch.performed += OnCrouchPerformed;
            _customInput.Player.Crouch.canceled += OnCrouchCancelled;

            _customInput.Player.Jump.performed += OnJumpPerformed;

            _customInput.Player.Interact.performed += OnInteractPerformed;
            _customInput.Player.Use.performed += OnUsePerformed;
            _customInput.Player.Drop.performed += OnDropPerformed;

            _customInput.Player.Pick.performed += OnPickPerformed;
            _customInput.Player.Pick.canceled += OnPickCancelled;

            _customInput.Player.NextUsable.performed += OnNextUsablePerformed;
            _customInput.Player.PreviousUsable.performed += OnPreviousUsablePerformed;
        }

        public void Dispose()
        {
            _customInput.Disable();
            _customInput.Dispose();

            _customInput.Player.Movement.performed -= OnMovementPerformed;
            _customInput.Player.Movement.canceled -= OnMovementCancelled;

            _customInput.Player.Sprint.performed -= OnSprintPerformed;
            _customInput.Player.Sprint.canceled -= OnSprintCancelled;

            _customInput.Player.Crouch.performed -= OnCrouchPerformed;
            _customInput.Player.Crouch.canceled -= OnCrouchCancelled;

            _customInput.Player.Jump.performed -= OnJumpPerformed;

            _customInput.Player.Interact.performed -= OnInteractPerformed;
            _customInput.Player.Use.performed -= OnUsePerformed;
            _customInput.Player.Drop.performed -= OnDropPerformed;
            
            _customInput.Player.Pick.performed -= OnPickPerformed;
            _customInput.Player.Pick.canceled -= OnPickCancelled;
            
            _customInput.Player.NextUsable.performed -= OnNextUsablePerformed;
            _customInput.Player.PreviousUsable.performed -= OnPreviousUsablePerformed;
        }

        public void SetInputEnabled(bool enabled)
        {
            if (enabled)
                _customInput.Player.Enable();
            else
                _customInput.Player.Disable();
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj) =>
            MovementPerformed?.Invoke(obj.ReadValue<Vector2>());

        private void OnJumpPerformed(InputAction.CallbackContext obj) => JumpPerformed?.Invoke();
        private void OnMovementCancelled(InputAction.CallbackContext obj) => MovementCancelled?.Invoke();
        private void OnCrouchCancelled(InputAction.CallbackContext obj) => CrouchCancelled?.Invoke();
        private void OnCrouchPerformed(InputAction.CallbackContext obj) => CrouchPerformed?.Invoke();
        private void OnSprintPerformed(InputAction.CallbackContext obj) => SprintPerformed?.Invoke();
        private void OnSprintCancelled(InputAction.CallbackContext obj) => SprintCancelled?.Invoke();

        private void OnDropPerformed(InputAction.CallbackContext obj) => DropPerformed?.Invoke();
        private void OnUsePerformed(InputAction.CallbackContext obj) => UsePerformed?.Invoke();
        private void OnInteractPerformed(InputAction.CallbackContext obj) => InteractPerformed?.Invoke();
        
        private void OnPickPerformed(InputAction.CallbackContext obj) => PickPerformed?.Invoke();
        private void OnPickCancelled(InputAction.CallbackContext obj) => PickCancelled?.Invoke();
        
        private void OnPreviousUsablePerformed(InputAction.CallbackContext obj) => PreviousUsablePerformed?.Invoke();
        private void OnNextUsablePerformed(InputAction.CallbackContext obj) => NextUsablePerformed?.Invoke();
    }
}