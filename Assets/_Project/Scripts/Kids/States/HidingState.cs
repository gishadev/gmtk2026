using gishadev.tools.StateMachine;
using UnityEngine;

namespace gishadev.gmtk.kids.States
{
    /// <summary>
    /// Kid walks to its assigned spot and stays hidden until found,
    /// periodically taunting on a randomized interval.
    /// </summary>
    public class HidingState : IState
    {
        private readonly Kid _kid;

        private float _tauntTimer;

        public HidingState(Kid kid) => _kid = kid;

        public void Tick()
        {
            _tauntTimer -= Time.deltaTime;
            if (_tauntTimer <= 0f)
            {
                _kid.PlayTaunt();
                ResetTauntTimer();
            }
        }

        public void OnEnter()
        {
            _kid.OnEnteredHiding();
            _kid.MoveToPOI(_kid.AssignedSpot);
            ResetTauntTimer();
        }

        public void OnExit()
        {
        }

        private void ResetTauntTimer()
        {
            var range = _kid.TauntDelayRange;
            _tauntTimer = Random.Range(range.x, range.y);
        }
    }
}
