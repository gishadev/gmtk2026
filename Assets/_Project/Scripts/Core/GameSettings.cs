using UnityEngine;

namespace gishadev.gmtk.Core
{
    public static class GameSettings
    {
        private const string MOUSE_SENSITIVITY_KEY = "MouseSensitivity";
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
    }
}
