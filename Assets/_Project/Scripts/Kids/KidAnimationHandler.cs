using System;
using gishadev.gmtk.Core;
using gishadev.gmtk.kids.States;
using UnityEngine;

namespace gishadev.gmtk.kids
{
    public class KidAnimationHandler : MonoBehaviour
    {
        private Kid _kid;
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _kid = GetComponent<Kid>();
        }

        private void LateUpdate() => _animator.SetInteger(Constants.KID_STATE_ANIM, (int)GetKidAnimationState());

        private KidAnimationState GetKidAnimationState()
        {
            switch (_kid.CurrentState)
            {
                case IdleState:
                    return KidAnimationState.Idle;
                case HappyState:
                    return KidAnimationState.Happy;
                case HidingState:
                    return KidAnimationState.Crouch;
                case RunningToNextLocationState:
                    return KidAnimationState.Run;
            }

            return KidAnimationState.Idle;
        }
    }

    public enum KidAnimationState
    {
        Idle = 0,
        Crouch = 1,
        Walk = 2,
        Run = 3,
        Happy = 4
    }
}