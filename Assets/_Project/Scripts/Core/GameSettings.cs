using UnityEngine;

namespace gishadev.gmtk.Core
{
    public static class GameSettings
    {
        private const string MOUSE_SENSITIVITY_KEY = "MouseSensitivity";
        private const string IS_GAME_WON_KEY = "IsGameWon";
        public const float DEFAULT_MOUSE_SENSITIVITY = 1f;

        public static float MouseSensitivity
        {
            get => PlayerPrefs.GetFloat(MOUSE_SENSITIVITY_KEY, DEFAULT_MOUSE_SENSITIVITY);
            set
            {
                PlayerPrefs.SetFloat(MOUSE_SENSITIVITY_KEY, value);
                PlayerPrefs.Save();
            }
        }

        public static bool IsGameWon
        {
            get => PlayerPrefs.GetInt(IS_GAME_WON_KEY, 0) == 1;
            set
            {
                PlayerPrefs.SetInt(IS_GAME_WON_KEY, value ? 1 : 0);
                PlayerPrefs.Save();
            }
        }
    }
}
