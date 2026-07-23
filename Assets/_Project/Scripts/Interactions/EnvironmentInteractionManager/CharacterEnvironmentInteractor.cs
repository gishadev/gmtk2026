using gishadev.gmtk.Core;
using gishadev.walkingSimulator.InteractionManager;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace gishadev.walkingSimulator.Interactions.EnvironmentInteractionManager
{
    public class CharacterEnvironmentInteractor : MonoBehaviour
    {
        [Inject] private CharacterInteractionDataSO _characterInteractionDataSO;
        [Inject] private IPlayerInputService _inputService;

        private Camera _cam;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void OnEnable()
        {
            _inputService.InteractPerformed += OnInteractPerformed;
        }

        private void OnDisable()
        {
            _inputService.InteractPerformed -= OnInteractPerformed;
        }

        private void OnInteractPerformed()
        {
            var ray = _cam.ScreenPointToRay(Mouse.current.position.value);

            if (Physics.SphereCast(ray, _characterInteractionDataSO.UniversalInteractRadius, out var hit,
                    _characterInteractionDataSO.EnvironmentInteractDistance))
            {
                if (hit.transform.gameObject.layer != LayerMask.NameToLayer(Constants.INTERACTABLE_LAYER_NAME))
                    return;

                if (!hit.transform.gameObject.TryGetComponent(out IInteractable interactable))
                    return;

                interactable.Interact();
            }
        }
    }
}