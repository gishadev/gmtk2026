using UnityEngine;

namespace gishadev.gmtk.Core
{
    public static class Constants
    {
        public const string INTERACTABLE_LAYER_NAME = "Interactable";
        public const string MOVING_PLATFORM_TAG = "MovingPlatform";
        public static readonly int KID_STATE_ANIM = Animator.StringToHash("State");
        public static readonly int IS_RAISED_HAND_ANIM = Animator.StringToHash("IsRaisedHand");
    }
}