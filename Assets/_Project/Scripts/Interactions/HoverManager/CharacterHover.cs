using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace gishadev.walkingSimulator.InteractionManager
{
    public class CharacterHover : MonoBehaviour
    {
        [Inject] private CharacterInteractionDataSO _characterInteractionDataSO;

        private Camera _cam;
        private IHoverable _lastHoverable;

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void LateUpdate()
        {
            var ray = _cam.ScreenPointToRay(Mouse.current.position.value);
            if (Physics.SphereCast(ray, _characterInteractionDataSO.UniversalInteractRadius, out var hit,
                    _characterInteractionDataSO.EnvironmentInteractDistance))
            {
                if (!hit.transform.gameObject.TryGetComponent(out IHoverable hoverable))
                {
                    ExitLastHoverable();
                    return;
                }

                hoverable.OnHoverEnter();
                _lastHoverable = hoverable;
            }
            else
                ExitLastHoverable();
        }

        private void ExitLastHoverable()
        {
            _lastHoverable?.OnHoverExit();
            _lastHoverable = null;
        }
    }
}