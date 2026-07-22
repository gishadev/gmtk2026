using gishadev.walkingSimulator.Core;
using gishadev.walkingSimulator.InteractionManager;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace gishadev.walkingSimulator.PhysicsDragManager
{
    public class CharacterPhysicsDrag : MonoBehaviour
    {
        [Inject] private CharacterInteractionDataSO _characterInteractionDataSO;
        [Inject] private IPlayerInputService _playerInputService;

        private Transform _jointTrans;
        private IPhysicsDraggable _currentDraggable;
        private float _dragDepth;

        private Camera _cam;
        private bool _dragging;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            _playerInputService.PickPerformed += OnPickPerformed;
            _playerInputService.PickCancelled += OnPickCancelled;
        }

        private void OnDisable()
        {
            _playerInputService.PickPerformed -= OnPickPerformed;
            _playerInputService.PickCancelled -= OnPickCancelled;
        }

        private void Update()
        {
            if (!_dragging || _jointTrans == null)
                return;

            _jointTrans.position =
                CameraPlaneConverter.ScreenToWorldPlanePoint(_cam, _dragDepth, Mouse.current.position.value);
            
            var distance = Vector3.Distance(_currentDraggable.transform.position, _jointTrans.position);
            if (distance > _characterInteractionDataSO.PhysicsPickableMaxDragDistance)
                BreakJoint();
        }


        private void OnPickPerformed()
        {
            var ray = _cam.ScreenPointToRay(Mouse.current.position.value);

            if (Physics.SphereCast(ray,_characterInteractionDataSO.UniversalInteractRadius, out var hit, _characterInteractionDataSO.PhysicsPickableDistance))
            {
                if (!hit.transform.gameObject.TryGetComponent(out _currentDraggable))
                    return;

                _dragDepth = CameraPlaneConverter.CameraToPointDepth(Camera.main, hit.point);
                _jointTrans = AttachJoint(hit.rigidbody, hit.point);
            }


            _dragging = true;
        }

        private void OnPickCancelled() => BreakJoint();

        private Transform AttachJoint(Rigidbody rb, Vector3 attachmentPosition)
        {
            GameObject go = new GameObject("Attachment Point");
            // go.hideFlags = HideFlags.HideInHierarchy;
            go.transform.position = attachmentPosition;

            var newRb = go.AddComponent<Rigidbody>();
            newRb.isKinematic = true;

            var joint = go.AddComponent<ConfigurableJoint>();
            joint.connectedBody = rb;
            joint.configuredInWorldSpace = true;
            joint.xDrive = NewJointDrive(_characterInteractionDataSO.PhysicsPickableForce,
                _characterInteractionDataSO.PhysicsPickableDamping);
            joint.yDrive = NewJointDrive(_characterInteractionDataSO.PhysicsPickableForce,
                _characterInteractionDataSO.PhysicsPickableDamping);
            joint.zDrive = NewJointDrive(_characterInteractionDataSO.PhysicsPickableForce,
                _characterInteractionDataSO.PhysicsPickableDamping);
            joint.slerpDrive = NewJointDrive(_characterInteractionDataSO.PhysicsPickableForce,
                _characterInteractionDataSO.PhysicsPickableDamping);
            joint.rotationDriveMode = RotationDriveMode.Slerp;

            return go.transform;
        }

        private void BreakJoint()
        {
            if (_jointTrans != null)
                Destroy(_jointTrans.gameObject);

            _dragging = false;
            _currentDraggable = null;
        }

        private JointDrive NewJointDrive(float force, float damping)
        {
            JointDrive drive = new JointDrive();
            drive.positionSpring = force;
            drive.positionDamper = damping;
            drive.maximumForce = Mathf.Infinity;
            return drive;
        }
    }
}