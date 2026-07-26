using UnityEngine;

namespace gishadev.gmtk.Core
{
    public static class Constants
    {
        public const string INTERACTABLE_LAYER_NAME = "Interactable";
        public const string MOVING_PLATFORM_TAG = "MovingPlatform";
        public const string FINAL_SCENE_NAME = "Final";
        public static string GAME_SCENE_NAME = "Game";
        public static string MENU_SCENE_NAME = "Menu";
        public static string INTRO_SCENE_NAME = "Intro";
        public static readonly int KID_STATE_ANIM = Animator.StringToHash("State");
        public static readonly int IS_RAISED_HAND_ANIM = Animator.StringToHash("IsRaisedHand");
        public static readonly int IS_COUNTING_ANIM = Animator.StringToHash("IsCounting");
    }
}