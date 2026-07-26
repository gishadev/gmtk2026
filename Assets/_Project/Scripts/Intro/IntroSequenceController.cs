using gishadev.gmtk.Core;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace gishadev.gmtk.Intro
{
    [RequireComponent(typeof(VideoPlayer))]
    public class IntroSequenceController : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private string videoFileName = "intro.mp4";

        private bool _transitioned;

        private void Reset() => videoPlayer = GetComponent<VideoPlayer>();

        private void Awake() =>
            videoPlayer.url = $"{Application.streamingAssetsPath}/{videoFileName}";
        private void OnEnable() => videoPlayer.loopPointReached += OnVideoFinished;
        private void OnDisable() => videoPlayer.loopPointReached -= OnVideoFinished;
        private void OnVideoFinished(VideoPlayer source) => GoToNextScene();

        private void Update()
        {
            if (_transitioned)
                return;

            if (AnyButtonPressed())
                GoToNextScene();
        }

        private static bool AnyButtonPressed()
        {
            var keyboard = Keyboard.current;
            if (keyboard != null && keyboard.anyKey.wasPressedThisFrame)
                return true;

            var gamepad = Gamepad.current;
            if (gamepad != null && gamepad.buttonSouth.wasPressedThisFrame)
                return true;

            return false;
        }

        private void GoToNextScene()
        {
            if (_transitioned)
                return;

            _transitioned = true;
            videoPlayer.Stop();

            SceneManager.LoadSceneAsync(Constants.GAME_SCENE_NAME);
        }
    }
}
