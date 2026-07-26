using UnityEngine;
using UnityEngine.InputSystem;

namespace gishadev.gmtk.Core
{
    public class FPSCameraController : MonoBehaviour
    {
        [SerializeField] private Transform body;
        [SerializeField] private float mouseSensitivity;

        private Rigidbody _bodyRigidbody;
        private float _xRot, _yRot;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;

            _bodyRigidbody = body.GetComponent<Rigidbody>();
            _yRot = body.eulerAngles.y;
        }

        private void Update()
        {
            // Camera Input. Mouse delta is already per-frame, so it must not be scaled by a
            // variable deltaTime; fixedDeltaTime is kept as a constant factor to preserve tuning.
            float sensitivity = mouseSensitivity * GameSettings.MouseSensitivity * Time.fixedDeltaTime;
            float mouseX = Mouse.current.delta.value.x * sensitivity;
            float mouseY = Mouse.current.delta.value.y * sensitivity;

            _yRot += mouseX;
            _xRot -= mouseY;
            _xRot = Mathf.Clamp(_xRot, -90f, 90f);

            // World-space rotation, so the interpolated rigidbody parent can't overwrite it.
            transform.rotation = Quaternion.Euler(_xRot, _yRot, 0f);
        }

        private void FixedUpdate()
        {
            // The body only yaws for movement direction; visually the camera already leads.
            _bodyRigidbody.MoveRotation(Quaternion.Euler(0f, _yRot, 0f));
        }
    }
}
