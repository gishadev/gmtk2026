using gishadev.tools.Effects;
using UnityEngine;
using VContainer;

namespace gishadev.gmtk.MovementManager
{
    /// <summary>
    /// Fires a step SFX every time this object travels a set distance on the horizontal plane.
    /// Actual sound playing is left as a placeholder — hook it up in <see cref="PlayStepSfx"/>.
    /// </summary>
    public class StepSfxHandler : MonoBehaviour
    {
        [Tooltip("Distance (in meters) the object must travel before a step SFX fires.")]
        [SerializeField] private float stepDistance = 2f;

        [Tooltip("Ignore vertical movement when measuring travelled distance.")]
        [SerializeField] private bool ignoreVertical = true;

        [Inject] private ISFXEmitter _sfxEmitter;
        
        private Vector3 _lastPosition;
        private float _accumulatedDistance;

        private void Start() => _lastPosition = transform.position;

        private void Update()
        {
            var current = transform.position;
            var delta = current - _lastPosition;
            if (ignoreVertical)
                delta.y = 0f;

            _accumulatedDistance += delta.magnitude;
            _lastPosition = current;

            if (_accumulatedDistance >= stepDistance)
            {
                _accumulatedDistance = 0f;
                PlayStepSfx();
            }
        }

        private void PlayStepSfx()
        {
            _sfxEmitter.EmitAt(SoundEffectsEnum.STEP_SOFT, transform.position, Quaternion.identity);
        }
    }
}
