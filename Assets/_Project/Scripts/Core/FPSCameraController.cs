using UnityEngine;
using UnityEngine.InputSystem;

namespace gishadev.gmtk.Core
{
    public class FPSCameraController : MonoBehaviour
    {
        [SerializeField] private Transform body;
        [SerializeField] private float mouseSensitivity;

        private float _xRot, _yRot;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            // Camera Input.
            float sensitivity = mouseSensitivity * GameSettings.MouseSensitivity;
            float mouseX = Mouse.current.delta.value.x * sensitivity * Time.fixedDeltaTime;
            float mouseY = Mouse.current.delta.value.y * sensitivity * Time.fixedDeltaTime;

            // Camera/Body Rotating.
            _xRot -= mouseY;
            _xRot = Mathf.Clamp(_xRot, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRot, 0f, 0f);

            _yRot = mouseX;
            body.Rotate(Vector3.up * _yRot);
        }
    }
}
