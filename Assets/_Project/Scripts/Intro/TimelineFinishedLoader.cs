using gishadev.gmtk.Core;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace gishadev.gmtk.Intro
{
    [RequireComponent(typeof(PlayableDirector))]
    public class TimelineFinishedLoader : MonoBehaviour
    {
        [SerializeField] private PlayableDirector director;

        private void Reset() => director = GetComponent<PlayableDirector>();
        private void OnEnable() => director.stopped += OnTimelineStopped;
        private void OnDisable() => director.stopped -= OnTimelineStopped;

        private void OnTimelineStopped(PlayableDirector source)
        {
            GameSettings.IsGameWon = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadSceneAsync(Constants.MENU_SCENE_NAME);
        }
    }
}
