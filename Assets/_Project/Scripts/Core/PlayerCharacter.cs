using UnityEngine;

namespace gishadev.gmtk.Core
{
    public class PlayerCharacter : MonoBehaviour
    {
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void Teleport(Vector3 position, Quaternion rotation)
        {
            if (_characterController != null)
                _characterController.enabled = false;

            transform.SetPositionAndRotation(position, rotation);

            if (_characterController != null)
                _characterController.enabled = true;
        }
    }
}
