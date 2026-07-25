using gishadev.tools.StateMachine;
using PrimeTween;
using UnityEngine;

namespace gishadev.gmtk.kids.States
{
    /// <summary>
    /// Kid has been caught for good and stops moving.
    /// </summary>
    public class HappyState : IState
    {
        private readonly Kid _kid;

        public HappyState(Kid kid) => _kid = kid;

        public void Tick()
        {
        }

        public void OnEnter()
        {
            _kid.Stop();

            var topPosition = _kid.transform.position + Vector3.up * 20f;
            var tween = Tween.Position(_kid.transform, topPosition, 15f);
            tween.OnComplete(() => Object.Destroy(_kid.gameObject));
        }

        public void OnExit()
        {
        }
    }
}